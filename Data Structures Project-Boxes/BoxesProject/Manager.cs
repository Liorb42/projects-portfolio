using BoxesProject.DataStructures;
using BoxesProject.Data;
using BoxesProject.Interfasces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;

namespace BoxesProject
{
    public class Manager
    {
        private BST<WidthX> _xTree;
        private DoubleLinkedList<TimeData> _boxCollectionByTime;
        private IUiNotifier _uiNotifier;
        private ConfigurationParams _configParams;      
        private Timer _timer;
        private List<BoxDetails> _allBoxesDisplay;

        public Manager(IUiNotifier uiNotifier, ConfigurationParams configParams)
        {
            _xTree = new BST<WidthX>();
            _boxCollectionByTime = new DoubleLinkedList<TimeData>();
            _uiNotifier = uiNotifier;
            _configParams = configParams;            
            InitTimer();   
        }       
        private void InitTimer() 
        {
            if (_timer != null) 
                _timer.Dispose();

            int due = (int)_configParams.DeleteTimeOfDay.Subtract(DateTime.Now).TotalMilliseconds; 

            int period = (int)new TimeSpan (1,0,0,0).TotalMilliseconds; // scan is done once a day

            _timer = new Timer(DeleteExpiredItems, null, due, period);
                    
        } 
        public void InsertIntoInventory(double x, double y, int amount)
        {
            if (!(x > 0 && y > 0 && amount > 0))
                throw new ArgumentException("All values must be above 0");

            //check if x is in tree. if not add.
            WidthX tmpX = new WidthX(x);
            _xTree.FindOrAdd(tmpX, out tmpX);

            //check if y is in the tree. if not add.
            HeightY tmpY = new HeightY(y);
            FoundOrAdded isDataAdded = tmpX.YTree.FindOrAdd(tmpY, out tmpY);

            //check if the box was added for the first time
            if (isDataAdded == FoundOrAdded.ADDED)
            {
                //add to collection by time
                _boxCollectionByTime.AddLast(new TimeData(tmpX.X, tmpY.Y));
                tmpY.TimeCollectionRef = _boxCollectionByTime.End;
                UpdateUI("New box was added", tmpX.X, tmpY.Y, amount, tmpY.TimeCollectionRef.data.LastAccessedDate);
            }
            // update amount 
            UpdateUnitsAmount(amount, tmpX, tmpY);

            if (isDataAdded == FoundOrAdded.FOUND)
                UpdateUI("Inventory was updated");                          
        }
        private void UpdateUnitsAmount(int amount, WidthX tmpX, HeightY tmpY)
        {
            tmpY.Amount += amount;
            // check amount doesn't exceed max capacity
            if (tmpY.Amount > _configParams.MaxCapacityPerBox)
            {
                UpdateUI($"Max capacity was exceeded. {tmpY.Amount - _configParams.MaxCapacityPerBox} unit(s) will be returned",
                    tmpX.X, tmpY.Y, _configParams.MaxCapacityPerBox, tmpY.TimeCollectionRef.data.LastAccessedDate);
                tmpY.Amount = _configParams.MaxCapacityPerBox;
            }
        }
        public bool DisplayBoxDetails(double x, double y)
        {
            if (x < 0 || y < 0)
            {
                UpdateUI("Invalid input");
                return false;
            }                

            //check if x is in the tree
            WidthX tmpX = new WidthX(x);
            if (_xTree.Search(tmpX, out tmpX))
            {
                //if yes, check if y is in the tree
                HeightY tmpY = new HeightY(y);
                if (tmpX.YTree.Search(tmpY, out tmpY))
                {
                    //if yes 
                    UpdateUI("Found box",tmpX.X, tmpY.Y, tmpY.Amount, tmpY.TimeCollectionRef.data.LastAccessedDate);
                    return true;
                }
            }
            UpdateUI("Box wasn't found");
            return false;

        }
        public void DisplayAllInventory()
        {
            _allBoxesDisplay = new List<BoxDetails>();
            _xTree.ScanInOrder(GetAllBoxesDetails);
            _uiNotifier.OnDisplayAllInventory(_allBoxesDisplay);
        }
        private void GetAllBoxesDetails(WidthX x)
        {
            x.YTree.ScanInOrder((y) =>
            {
                BoxDetails boxToDisplay = new BoxDetails(x.X, y.Y, y.Amount, y.TimeCollectionRef.data.LastAccessedDate);
                _allBoxesDisplay.Add(boxToDisplay);                
            });
        }
        private void UpdateUI(string message, double x = 0, double y = 0, int amount = 0, DateTime lastAccessDate = default)
        {
            BoxDetails boxToSend;
            if (lastAccessDate == default) boxToSend = null;
            else boxToSend = new BoxDetails(x, y, amount, lastAccessDate);

            _uiNotifier.OnMessage(message, boxToSend);
        }
        public void Buy(double x, double y, int requestedAmount, int allowedSplits)
        {
            WidthX searchX = new WidthX(x);
            WidthX divMarginX = new WidthX(x * (1+_configParams.SearchDivMargin));
            HeightY searchY = new HeightY(y);
            HeightY divMarginY = new HeightY(y * (1+ _configParams.SearchDivMargin));
            double currentXValue = x;
            double currentYValue = y;

            List<BoxDetails> foundBoxesForUI = new List<BoxDetails>();
            List<(WidthX x, HeightY y, int requestedAmount)> foundBoxesData = new List<(WidthX, HeightY, int)>();
            int splitsCount = 0;
                        
            while (searchX.CompareTo(divMarginX) <= 0 && allowedSplits >= splitsCount)
            {
                //find x
                if (_xTree.FindExactOrClosest(searchX, divMarginX, out WidthX foundX))
                {
                    currentXValue = foundX.X;

                    while (searchY.CompareTo(divMarginY) <= 0 && allowedSplits >= splitsCount)
                    {
                        //find y
                        if (foundX.YTree.FindExactOrClosest(searchY, divMarginY, out HeightY foundY))
                        {
                            //found 1 Box
                            currentYValue = foundY.Y;
                            foundBoxesForUI.Add(new BoxDetails(foundX.X, foundY.Y, foundY.Amount, foundY.TimeCollectionRef.data.LastAccessedDate));

                            //check if amount is sufficient
                            if (foundY.Amount >= requestedAmount)
                            {
                                //Save box data
                                foundBoxesData.Add((foundX, foundY, requestedAmount));                                

                                //ask user if they want to proceed
                                if (_uiNotifier.OnGetUserResponse(foundBoxesForUI))
                                {
                                    for (int i = 0; i < foundBoxesData.Count; i++)
                                    {
                                        foundBoxesData[i].y.Amount -= foundBoxesData[i].requestedAmount;
                                        //check if the box is out of stock and will be deleted
                                        if (CheckAmountLevels(foundBoxesData[i].x, foundBoxesData[i].y) == isDeletedFromStock.FALSE)
                                        {
                                            foundBoxesData[i].y.TimeCollectionRef.data.LastAccessedDate = DateTime.Now;
                                            _boxCollectionByTime.MoveNodeToLast(foundBoxesData[i].y.TimeCollectionRef);
                                        }
                                    }                                    
                                    UpdateUI("Purchase completed");
                                    return;
                                }
                                else return;
                            }                            
                            else
                            {
                                //Save box data and it's full amount available
                                foundBoxesData.Add((foundX, foundY, foundY.Amount));
                                requestedAmount -= foundY.Amount;
                                currentYValue += 0.1;
                                searchY = new HeightY(currentYValue);
                                splitsCount++;
                            }
                        }
                        else
                            break;
                    }
                }
                currentXValue += 0.1;
                searchY = new HeightY(y);
                searchX = new WidthX(currentXValue);
            }
            UpdateUI("Could not find boxes that match search parameters");
        }
        private isDeletedFromStock CheckAmountLevels(WidthX x, HeightY y)
        {
            if (y.Amount == 0)
            {                
                _uiNotifier.OnDeletionFromStock($"The following box was out of stock and was deleted from the stock list",
                    new BoxDetails(x.X, y.Y,y.Amount,y.TimeCollectionRef.data.LastAccessedDate));
                DeleteFromStock(x, y);
                return isDeletedFromStock.TRUE;
            }
            if (y.Amount <= _configParams.AlertUnitLimit && y.Amount != 0)
            {
                _uiNotifier.OnOutOfStockAlert($"The following box is running low on stock",
                   new BoxDetails(x.X, y.Y, y.Amount, y.TimeCollectionRef.data.LastAccessedDate));
            }
            return isDeletedFromStock.FALSE;
        }
        private void DeleteFromStock(WidthX x, HeightY y)
        {
            //remove from time collection
            _boxCollectionByTime.MoveNodeToLast(y.TimeCollectionRef);
            _boxCollectionByTime.RemoveLast(out TimeData removedData);

            //remove y
            x.YTree.Remove(y, out y);

            //if there are no more y - delete x
            if (x.YTree.GetDepth() == 0)
                _xTree.Remove(x, out x);
        }
        private void DeleteExpiredItems(Object param)
        {
            UpdateUI("Scan for expired itmes was initiated");
            while (true)
            {                
                //get the first item and check if it is expired
                _boxCollectionByTime.GetAt(0, out TimeData tmpTime);
                if ((DateTime.Now - tmpTime.LastAccessedDate).TotalDays > _configParams.ExpirationInterval)
                {
                    //remove from time collection
                    _boxCollectionByTime.RemoveFirst(out tmpTime);                   

                    _xTree.Search(new WidthX(tmpTime.X), out WidthX tmpX);
                    HeightY tmpY = new HeightY(tmpTime.Y);                    

                    //remove y 
                    tmpX.YTree.Remove(tmpY, out tmpY);

                    //if there are no more y - delete x
                    if (tmpX.YTree.GetDepth() == 0)
                        _xTree.Remove(tmpX, out tmpX);

                    _uiNotifier.OnDeletionFromStock($"The following box has expired and was deleted from the stock list",
                    new BoxDetails(tmpTime.X, tmpTime.Y, tmpY.Amount, tmpTime.LastAccessedDate));
                }
                else return;
            }  
        }         
        public void UpdateConfiguration (ConfigurationParams newConfig)
        {
            if (newConfig != null)
            {
                //check if the new config values require inventory scan
                bool isScanNeeded = false;
                if (newConfig.MaxCapacityPerBox < _configParams.MaxCapacityPerBox || 
                    newConfig.AlertUnitLimit > _configParams.AlertUnitLimit)
                    isScanNeeded = true;

                _configParams = newConfig;
                InitTimer();
                if(isScanNeeded)
                    _xTree.ScanInOrder(CheckBoxesUnitsAmount);
                DeleteExpiredItems(null);

                UpdateUI("New Configuration was set");
            }            
        }
        private void CheckBoxesUnitsAmount(WidthX x)
        {
            x.YTree.ScanInOrder((y) =>
            {
                if(y.Amount > _configParams.MaxCapacityPerBox)
                {
                    UpdateUnitsAmount(0, x, y);
                }  
                if(y.Amount <= _configParams.AlertUnitLimit)
                {
                    _uiNotifier.OnOutOfStockAlert($"The following box is running low on stock",
                   new BoxDetails(x.X, y.Y, y.Amount, y.TimeCollectionRef.data.LastAccessedDate));
                }
            });
        }
        public void Exit()
        {
            _timer.Dispose();
        }
    }
}
