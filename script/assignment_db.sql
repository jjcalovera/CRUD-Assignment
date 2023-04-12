DROP DATABASE IF EXISTS assignment_db;
CREATE DATABASE assignment_db;

USE assignment_db;

CREATE TABLE genders(
    id INT NOT NULL AUTO_INCREMENT,
    gender VARCHAR(45) NOT NULL,
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY(id)
);

INSERT INTO genders(gender) VALUES("Male");
INSERT INTO genders(gender) VALUES("Female");

CREATE TABLE users(
    id INT NOT NULL AUTO_INCREMENT,
    profilePicture BLOB DEFAULT NULL,
    firstName VARCHAR(45) NOT NULL,
    middleName VARCHAR(45) DEFAULT NULL,
    lastName VARCHAR(45) NOT NULL,
    genderFID INT NOT NULL,
    age INT NOT NULL,
    birthday DATE,
    contactNumber VARCHAR(45) DEFAULT NULL,
    email VARCHAR(45) DEFAULT NULL,
    username VARCHAR(45) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY(id),
    FOREIGN KEY(genderFID) REFERENCES genders(id)
        ON UPDATE CASCADE ON DELETE CASCADE
);

INSERT INTO users(firstName, middleName, lastName, genderFID, age, birthday, contactNumber, email, username, password)
VALUES("Joven Joshua", "Celiz", "Alovera", 1, 23, "1999-10-25", "09434071429", "jjcalovera@gmail.com", "admin", MD5("admin"));

CREATE TABLE customers(
    id INT NOT NULL AUTO_INCREMENT,
    firstName VARCHAR(45) NOT NULL,
    middleName VARCHAR(45) DEFAULT NULL,
    lastName VARCHAR(45) NOT NULL,
    genderFID INT NOT NULL,
    age INT NOT NULL,
    birthday DATE,
    contactNumber VARCHAR(45) DEFAULT NULL,
    email VARCHAR(45) DEFAULT null,
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY(id),
    FOREIGN KEY(genderFID) REFERENCES genders(id)
        ON UPDATE CASCADE ON DELETE CASCADE
);