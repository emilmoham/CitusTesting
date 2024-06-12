DELETE FROM accounting.dbo.entries;
DBCC CHECKIDENT ('[accounting].[dbo].[entries]', RESEED, 0);
DELETE FROM accounting.dbo.transactions;
DBCC CHECKIDENT ('[accounting].[dbo].[transactions]', RESEED, 0);
DELETE FROM accounting.dbo.accounts;
DBCC CHECKIDENT ('[accounting].[dbo].[accounts]', RESEED, 0);
DELETE FROM accounting.dbo.facilities;
DBCC CHECKIDENT ('[accounting].[dbo].[facilities]', RESEED, 0);

DROP TABLE accounting.dbo.entries;
DROP TABLE accounting.dbo.transactions;
DROP TABLE accounting.dbo.accounts;
DROP TABLE accounting.dbo.facilities;