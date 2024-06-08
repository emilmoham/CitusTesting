DELETE FROM entries;
alter sequence accounting.public.entries_id_seq restart with 1;

DELETE FROM transactions;
alter sequence accounting.public.transactions_id_seq restart with 1;

DELETE FROM accounts;
alter sequence accounting.public.accounts_id_seq restart with 1;

DELETE FROM facilities;
alter sequence accounting.public.facilities_id_seq restart with 1;

--drop table entries;
--drop table transactions;
--drop table accounts;
--drop table facilities;
