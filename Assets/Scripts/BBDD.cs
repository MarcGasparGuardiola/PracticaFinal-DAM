﻿
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
 

public class BBDD
{
    //La connexio pot ser statica ja que sempre sera la mateixa per totes les connexions
    public string conn = "URI=file:" + Application.dataPath + "/baseDadesSQLitle.db"; //Path to database.
    public void SelectTest() 
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT ID_User,mail, password, height, weight, maxFC, maxW  " + "FROM user";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string mail = reader.GetString(1);
            string password = reader.GetString(2);
            int height = reader.GetInt32(3);
            int weight = reader.GetInt32(4);
            int maxFC = reader.GetInt32(5);
            int maxW = reader.GetInt32(6);

            Debug.Log("id= " + id + "  email=" + mail + "  password=" + password + "  height=" + height + "  weight=" + weight + "  maxFC=" + maxFC + "  maxW=" + maxW);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }


    public int insertUser(String mail, String password, int height, int weight) {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.

        IDbCommand dbcmd = dbconn.CreateCommand();
        string insertUserQuery = "INSERT INTO User(ID_User,mail, password, height, weight, maxFC, maxW) VALUES(null, '"+ mail +"','"+ password+"'," + height+","+weight+","+100+","+250+");";

        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = insertUserQuery;

        int error = 0;
        try
        {
            IDataReader reader = dbcmd.ExecuteReader();
            Console.WriteLine("row inserted");
            reader.Close();
            reader = null;
        }
        catch (Exception ex) {
            if (ex.Message.Contains("UNIQUE constraint failed: user.mail"))
            {
                Debug.Log("Email repetit " + ex.Message);
                error = 1; //ID_Error == 1 (Email ja existeix en la base de dades)
                //No podem fer un return aqui ja que hem de tancar la base de dades
            }
            else {
                Debug.Log("ERROR desconegut " + ex.Message);
            }
        }
        
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return error;
    }


    public int comprovarCredencials(String mail, String password)
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.

        IDbCommand dbcmd = dbconn.CreateCommand();
        string insertUserQuery = "SELECT mail, password FROM  user WHERE mail='" + mail + "' AND password='" + password + "';";

        dbcmd = dbconn.CreateCommand();
        dbcmd.CommandText = insertUserQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        int count = 0;
        while (reader.Read())
        {
            count++;
        }

        int error;
        if (count == 1)
        {
            error = 1; //ID return 1 == s'ha trobat una coincidencia
        }
        else if (count == 0)
        {
            error = 0; //ID return 0 == NO s'ha trobat una coincidencia
        }
        else
        {
            error = 2; //ID return 2 == S'ha trobat més d'una coincidencia, No hauria de passar ja que el correu es unic
                       //Per si acas ho deixo aqui
        }

        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return error;
    }

    public User selectUser(String mail)
    {
        //Variables per crear l'usuari
        int id;
        string email;
        string password;
        int height;
        int weight;
        int maxFC;
        int maxW;

        User user = null;


        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();

        //Només noecessitem el correu ja que al ser unic no hi hauran més d'un usuari amb el mateix correu
        //A part el selectUser només s'ha de fer un cop sabem que les credencials son correctes
        string sqlQuery = "SELECT * FROM  user WHERE mail='" + mail + "';";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            id = reader.GetInt32(0);
            email = reader.GetString(1);
            password = reader.GetString(2);
            height = reader.GetInt32(3);
            weight = reader.GetInt32(4);
            maxFC = reader.GetInt32(5);
            maxW = reader.GetInt32(6);

            Debug.Log("id= " + id + "  email=" + email + "  password=" + password + "  height=" + height + "  weight=" + weight + "  maxFC=" + maxFC + "  maxW=" + maxW);
        
            user = new User(id, email, password, height, weight, maxFC, maxW);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return user;
    }
}
