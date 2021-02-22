CREATE DATABASE Cars_data
USE Cars_data

CREATE TABLE VehicleMake
(
ID INT PRIMARY KEY IDENTITY,
Name nvarchar(30),
Abrv nvarchar(20)
)

CREATE TABLE VehicleModel
(
ID INT PRIMARY KEY IDENTITY,
Name nvarchar(30),
Abrv nvarchar(20),
MakeID INT FOREIGN KEY REFERENCES VehicleMake(ID)
)

INSERT INTO VehicleMake VALUES('BMW', 'BMW')
INSERT INTO VehicleMake VALUES('Volkswagen', 'VW')
INSERT INTO VehicleMake VALUES('Ford', 'Ford')


INSERT INTO VehicleModel VALUES('128', 'BMW', 1)
INSERT INTO VehicleModel VALUES('Fiesta', 'Ford', 3)
INSERT INTO VehicleModel VALUES('Golf 5', 'VW', 2)
INSERT INTO VehicleModel VALUES('Focus', 'Ford', 3)
INSERT INTO VehicleModel VALUES('325', 'BMW', 1)
INSERT INTO VehicleModel VALUES('Golf 6', 'VW', 2)
INSERT INTO VehicleModel VALUES('X5', 'BMW', 1)
INSERT INTO VehicleModel VALUES('Golf 7', 'VW', 2)
INSERT INTO VehicleModel VALUES('Mondeo', 'Ford', 3)