package com.example.demo;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class databaseconnection{

    public Connection connect(){
        try(Connection connection = DriverManager.getConnection("jdbc:postgresql://localhost:5432/wordcount", "postgres", "1234")) {
            return connection;
        }
        catch (SQLException throwables) {
            throwables.printStackTrace();
        } ;
        System.out.println("Connected to PostgreSQL database!");
        return null;

        //Statement statement = connection.createStatement();
    }
}
