create table facilities (
	id serial primary key,
	name text not null
);

--insert into facilities (name) values ('Site 1');

--select * from facilities;

create table accounts (
	id serial primary key,
	name text not null,
	number int not null,
	type bit not null,
	facility_id int not null references facilities
);

--insert into accounts (name,  number, type, facility_id) values ('assets', 1000, '0', 1);
--insert into accounts (name,  number, type, facility_id) values ('liabilities', 2000, '1', 1);

--select * from accounts;

create table transactions (
	id bigserial primary key,
	facility_id int not null references facilities
);

--insert into transactions(facility_id) values (1);

--select * from transactions;


create table entries (
	id bigserial primary key,
	transaction_id bigint not null references transactions,
	account_id int not null references accounts,
	credit int not null,
	debit int not null
);

--insert into entries (transaction_id, account_id, credit, debit) values (1, 1, 100, 0), (1, 2, 0, 100);

--select * from entries;

--drop table entries;
--drop table transactions;
--drop table accounts;
--drop table facilities;
