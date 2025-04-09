CREATE TABLE `User`
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    UserName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE, 
    Password VARCHAR(100) NOT NULL,
    CreateDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Active BOOLEAN,
    IsDeleted BOOLEAN,
    PersonId INT 
);

CREATE TABLE Person 
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(20) NOT NULL,
    Active BOOLEAN,
    IsDeleted BOOLEAN
);

CREATE TABLE Rol 
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Description TEXT,
    Active BOOLEAN,
    IsDeleted BOOLEAN
);

CREATE TABLE RolUser
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    RolId INT,
    UserId INT,
    IsDeleted BOOLEAN
);

CREATE TABLE Permission
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Description TEXT,
    Active BOOLEAN,
    IsDeleted BOOLEAN
);

CREATE TABLE Form 
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Description TEXT,
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Active BOOLEAN,
    IsDeleted BOOLEAN
);

CREATE TABLE FormModule
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    FormId INT,
    ModuleId INT,
    IsDeleted BOOLEAN
);

CREATE TABLE RolFormPermission
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    RolId INT,
    FormId INT,
    PermissionId INT,
    IsDeleted BOOLEAN
);

CREATE TABLE Module
(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Description TEXT, 
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Active BOOLEAN,
    IsDeleted BOOLEAN
);

-- Relaciones 


-- User  - Person
ALTER TABLE `User` 
ADD CONSTRAINT FK_User_Person 
FOREIGN KEY (PersonId) REFERENCES Person(Id);

-- RolUser - Rol - User
ALTER TABLE RolUser
ADD CONSTRAINT FK_RolUser_Rol 
FOREIGN KEY (RolId) REFERENCES Rol(Id);

ALTER TABLE RolUser 
ADD CONSTRAINT FK_RolUser_User
FOREIGN KEY (UserId) REFERENCES `User`(Id);

-- FormModule - Form - Module

ALTER TABLE FormModule 
ADD CONSTRAINT FK_FormModule 
FOREIGN KEY (FormId) REFERENCES Form(Id);

ALTER TABLE FormModule 
ADD CONSTRAINT FK_FormModule_Module 
FOREIGN KEY (ModuleId) REFERENCES Module(Id);

-- RolFormPermission - Rol - Form - Permission
ALTER TABLE RolFormPermission 
ADD CONSTRAINT FK_RolFormPermission_Rol 
FOREIGN KEY (RolId) REFERENCES Rol(Id);

ALTER TABLE RolFormPermission 
ADD CONSTRAINT FK_RolFormPermission_Form
FOREIGN KEY (FormId) REFERENCES Form(Id);

ALTER TABLE RolFormPermission 
ADD CONSTRAINT FK_RolFormPermission
FOREIGN KEY (PermissionId) REFERENCES Permission(Id);

