INSERT INTO accounting.dbo.facilities (Name) values
	('Boston'),
	('Cheyenne'),
	('Des Moines'),
	('Jackson'),
	('New York'),
	('Pheonix'),
	('Philadelphia'),
	('Sacramento'),
	('Salem'),
	('Trenton')

CREATE TABLE #account_temp(
	Name varchar(50),
	Number int,
	Type bit,
)

--SELECT * FROM #account_temp as a right join accounting.dbo.facilities as f on 1=1;

INSERT INTO #account_temp 
	(Name, Number, [Type]) 
VALUES 
	('Assets', 1000, 0),
	('Liabilities', 2000, 1),
	('Equity', 3000, 1),
	('Revenue', 4000, 1),
	('Expenses', 5000, 0)

INSERT INTO accounting.dbo.accounts	
	(Name, Number, [Type], FacilityId)
SELECT a.Name, a.Number, a.[Type], f.id FROM #account_temp as a right join [accounting].[dbo].[facilities] as f on 1=1