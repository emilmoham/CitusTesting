DELETE FROM accounting.public.entries;

DELETE FROM accounting.public.transactions;

DELETE FROM accounting.public.accounts;

DELETE FROM accounting.public.facilities;


select undistribute_table('entries', cascade_via_foreign_keys=>true);
--select undistribute_table('transactions', cascade_via_foreign_keys=>true);
--select undistribute_table('accounts');
--select undistribute_table('facilities');

DROP TABLE entries;
DROP TABLE transactions;
DROP TABLE accounts;
DROP TABLE facilities;