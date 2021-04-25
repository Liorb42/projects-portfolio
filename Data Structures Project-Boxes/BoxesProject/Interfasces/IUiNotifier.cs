using BoxesProject.Data;
using BoxesProject.DataStructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BoxesProject.Interfasces
{
    public interface IUiNotifier
    {        
        void OnMessage (string message, BoxDetails box);
        void OnOutOfStockAlert(string message, BoxDetails box);
        bool OnGetUserResponse(List<BoxDetails> foundBoxesList);
        void OnDeletionFromStock(string message, BoxDetails box);
        void OnDisplayAllInventory(List<BoxDetails> boxesList);

    }
}
