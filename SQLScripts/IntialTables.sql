CREATE DATABASE Adventure4You;
GO

USE Adventure4You;
GO

CREATE TABLE UserLinks (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UserId NVARCHAR(450) NOT NULL,
	TeamId INT,
	RaceId INT
);

CREATE TABLE Races (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    CoordinatesCheckEnabled BIT NOT NULL,
	SpecialTasksAreStage BIT NOT NULL,
	MaximumTeamSize INT NOT NULL,
	MimimumPointsToCompleteStage INT NOT NULL,
	StartTime DATETIME2,
	EndTime DATETIME2
);

CREATE TABLE Stages (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Name NVARCHAR(255) NOT NULL	
);

CREATE TABLE StageLinks (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	StageId INT NOT NULL,
	RaceId INT NOT NULL
);

CREATE TABLE Points (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Type INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
	Value INT NOT NULL,
    Coordinates NVARCHAR(255)
);

CREATE TABLE PointLinks (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	PointId INT NOT NULL,
	StageId INT NOT NULL
);

CREATE TABLE Teams (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);

CREATE TABLE TeamLinks (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	TeamId INT NOT NULL,
    RaceId INT NOT NULL
);

CREATE TABLE TeamPointsVisited (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	PointId INT NOT NULL,
	TeamId INT NOT NULL,
	Time DATETIME2 NOT NULL
);

CREATE TABLE TeamStagesFinished (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	StageId INT NOT NULL,
	TeamId INT NOT NULL,
	StartTime DATETIME2 NOT NULL,
	StopTime DATETIME2 NOT NULL,
);