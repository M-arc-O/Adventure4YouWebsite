CREATE DATABASE Adventure4You;
GO

USE Adventure4You;
GO

CREATE TABLE Users (
	UserId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UserName NVARCHAR(255) NOT NULL,
	UserEmail NVARCHAR(255) NOT NULL,
	UserPassword NVARCHAR(64) NOT NULL,
);

CREATE TABLE UserLinks (
	UserLinkId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UserLinkTeamId INT NOT NULL,
	UserLinkRaceId INT NOT NULL
);

CREATE TABLE Races (
    RaceId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    RaceName NVARCHAR(255) NOT NULL,
    RaceCoordinatesCheckEnabled BIT
);

CREATE TABLE Points (
    PointId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    PointRaceId INT NOT NULL,
    PointName NVARCHAR(255) NOT NULL,
	PointValue INT NOT NULL,
    PointCoordinates NVARCHAR(255)
);

CREATE TABLE PointLinks (
    PointLinkId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	PointLinkVisitedTeamId INT NOT NULL
);

CREATE TABLE Teams (
    TeamId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TeamName NVARCHAR(255) NOT NULL
);

CREATE TABLE TeamLinks (
    TeamLinkId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	TeamLinkTeamId INT NOT NULL,
    TeamLinkRaceId INT NOT NULL
);