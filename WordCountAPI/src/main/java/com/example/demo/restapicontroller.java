package com.example.demo;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.http.MediaType;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.util.Collection;
import java.util.List;
import java.util.Map;
import java.sql.*;
import javax.sql.*;
@SpringBootApplication
@RestController
public class restapicontroller {
    @Autowired
    JdbcTemplate jdbc;
    @PostMapping(value = "/wordCountData", consumes = {MediaType.APPLICATION_JSON_VALUE})
    public String insert(@RequestBody String input) throws ParseException, SQLException {
        String word, filepath, articletitle;
        List<Map<String, Object>> rows = jdbc.queryForList("SELECT * FROM wordlist");
        //System.out.println(rows);
        JSONParser parser = new JSONParser();
        JSONObject object = (JSONObject) parser.parse(input);
        //word = (String) object.get("word");
        System.out.println(" word: " + object.get("word") + '\n'
                + " filepath: " + object.get("filepath") + '\n'
                + " articletitle: " + object.get("articletitle") + '\n'
                + " totalwordsinarticle: " + object.get("totalwordsinarticle"));
        Connection connection = DriverManager.getConnection("jdbc:postgresql://localhost:5432/wordcount", "postgres", "1234");
        if(object.get("word") != null);
        {
            insertworddata(connection, "wordlist(wordname)", (String) object.get("word"));
        }
        if(object.get("filepath") != null && object.get("articletitle") != null && object.get("totalwordsinarticle") != null);
        {
            insertfiledata(connection,"filelist(filepath, articletitle, totalwordsinarticle)",
                    (String) object.get("filepath"),
                    (String) object.get("articletitle"),
                    Integer.parseInt(object.get("totalwordsinarticle").toString()));
        }
        if(object.get("amount") != null && object.get("wordname") != null && object.get("filepath") != null && object.get("articletitle") != null)
        {
            //insert data; amount, wordname, filepath, articletitle
            insertappearsdata(connection, "appearsin(amount, filepath, articletitle, wordname))",
                    Integer.parseInt(object.get("amount").toString()),
                    (String) object.get("filepath"),
                    (String) object.get("articletitle"),
                    (String) object.get("wordname")
            );
        }
        if (false) {
            throw new IllegalArgumentException(" Invalid input");
        }
        else {
            return "Values inserted";
        }
    }
    public int insertfiledata(Connection connection, String tableref, String datavalue1,
                              String datavalue2, int datavalue3) throws SQLException {
        String sql = String.format("INSERT INTO %s" + "VALUES ('%s','%s',%d)", tableref,datavalue1, datavalue2, datavalue3);
        Statement stmt = null;
        stmt = connection.createStatement();
        jdbc.update(sql);
        return 1;
    }
    public int insertworddata(Connection connection, String tableref, String word) throws SQLException {
        String sql = String.format("INSERT INTO %s " + "VALUES ('%s') ON CONFLICT DO NOTHING",tableref, word);
        Statement stmt = null;
        stmt = connection.createStatement();
        jdbc.update(sql);
        return 1;
    }
    public int insertappearsdata(Connection connection, String tableref, int amount,
                                 String filepath, String filename, String word) throws SQLException {
        String sql = String.format("INSERT INTO %s" + "VALUES (%d, '%s', '%s', '%s')", tableref, amount, filepath, filename, word);
        Statement stmt = null;
        stmt = connection.createStatement();
        jdbc.update(sql);
        return 1;
    }
}