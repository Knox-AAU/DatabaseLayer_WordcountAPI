package com.example.demo;

import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.jdbc.core.JdbcTemplate;

import java.sql.*;
import java.util.Arrays;
import java.util.Collection;
import java.util.Map;

@SpringBootApplication
public class DemoApplication {
    @Autowired
    JdbcTemplate jdbcTemplate;
    public static void main(String[] args) throws ParseException, SQLException {

        SpringApplication.run(DemoApplication.class, args);
        restapicontroller controller = new restapicontroller();
        databaseconnection conn = new databaseconnection();



        //databaseconnection psqlconnection = new databaseconnection();
        //psqlconnection.connect();

        //word, filepath, articletitle, totalwordsinarticle;

/*
        try {
            Connection conn = DriverManager.getConnection("jdbc:postgresql://localhost:5432/wordcount", "postgres", "1234");

            String sql = "INSERT INTO wordlist VALUES(?::JSON)";
            PreparedStatement ps = conn.prepareStatement(sql);

            for (int i = 0; i < 4; i++){
                ps.setInt (1, i+1);
                ps.setObject(1, texttojason[i]);
                ps.executeUpdate();
            }
        }
        catch(Exception e) {
            System.out.println(e.getMessage());
            e.printStackTrace();
        }
        */
    }
}


