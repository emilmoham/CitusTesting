INSERT INTO facilities (Name) values
	('Boston'),
	('Cheyenne'),
	('Des Moines'),
	('Jackson'),
	('New York'),
	('Pheonix'),
	('Philadelphia'),
	('Sacramento'),
	('Salem'),
	('Trenton');
	
CREATE TEMPORARY TABLE account_temp(
	name varchar(50),
	number int,
	type boolean
);

INSERT INTO account_temp 
	(name, number, type) 
VALUES 
	('Assets', 1000, false),
	('Liabilities', 2000, true),
	('Equity', 3000, true),
	('Revenue', 4000, true),
	('Expenses', 5000, false);

--SELECT * FROM account_temp as a right join facilities as f on 1=1;


INSERT INTO accounts	
	(name, number, type, facility_id)
SELECT a.name, a.number, a.type, f.id FROM account_temp as a right join facilities as f on 1=1;

DROP TABLE account_temp;