const sqlite3 = require('sqlite3').verbose();
const { logError } = require('../services/loggerService');
const User = require('../models/userModel');

let db;
const connect = () => {
  db = new sqlite3.Database('./db/db.db', sqlite3.OPEN_READWRITE, (err) => {
    if (err) {
      logError(`Error Message : ${err}`);
    }
    console.log('Connected to the SQlite database.');
  });
};
const disconnect = () => {
  db.close((err) => {
    if (err) {
      logError(`Error Message : ${err}`);
    }
    console.log('Close the database connection.');
  });
};
const validateUniqueValue = async (cloumn, value) => {
  connect();
  const result = await new Promise((resolve, reject) => {
    let query = `SELECT * from Users where ${cloumn} = ?`;
    db.get(query, [value], async (err, row) => {
      if (err) {
        console.log(err);
        reject(false);
      } else {
        if (row) {
          console.log(row);
          resolve(false);
        }
        resolve(true);
      }
    });
  });
  disconnect();
  return result;
};

module.exports = {
  async getUserFromDb(name, password) {
    connect();
    const result = await new Promise((resolve, reject) => {
      let query = `SELECT * from Users where Name = ? AND Password = ?`;
      let user;
      db.get(query, [name, password], async (err, row) => {
        if (err) {
          console.log(err);
          logError(`Error Message : ${err}`);
          reject(err);
        } else {
          if (row) {
            user = new User(row.Id, row.Name, row.Email);
            resolve(user);
          }
          resolve(row);
        }
      });
    });
    disconnect();
    return result;
  },
  async registerUser(name, email, password) {
    //check if the name already exists on the db
    let isValueValid = await validateUniqueValue('Name', name);
    if (!isValueValid) {
      return 'name already exist. please choose a different name';
    }
    //check if the email already exists on the db
    isValueValid = await validateUniqueValue('Email', email);
    if (!isValueValid) {
      return 'email already exist. please register with a different email';
    }

    connect();
    const result = await new Promise((resolve, reject) => {
      db.run(
        `INSERT INTO Users (Name, Email, Password) VALUES (?,?,?) `,
        [name, email, password],
        (err) => {
          if (err !== null) {
            console.log(err);
            logError(`Error Message : ${err}`);
            disconnect();
            reject(err);
          } else {
            resolve('Finished writing player to db');
          }
        }
      );
    });

    console.log(result);
    return 'player registered. log-in to play';
  },
};
