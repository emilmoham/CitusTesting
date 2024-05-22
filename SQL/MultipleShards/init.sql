CREATE TABLE facilities (
	id serial PRIMARY KEY,
	name text NOT NULL
);

create table accounts (
	id serial NOT NULL,									-- Removed PRIMARY KEY
	name text NOT NULL,
	number int NOT NULL,
	type boolean NOT NULL,
	facility_id int NOT NULL REFERENCES facilities(id),
	PRIMARY KEY (facility_id, id)						-- Added composite PRIMARY KEY
);

create table transactions (
	id BIGSERIAL NOT NULL,								-- Removed PRIMARY KEY
	facility_id INT NOT NULL REFERENCES facilities(id),
	PRIMARY KEY (facility_id, id)						-- Added composite PRIMARY KEY
);


CREATE TABLE entries (
	id bigserial NOT NULL,								-- Removed PRIMARY KEY
	facility_id int NOT NULL REFERENCES facilities(id),
	transaction_id bigint NOT NULL ,					-- Removed FOREIGN KEY
	account_id int NOT NULL,							-- Removed FOREIGN KEY 
	credit int NOT NULL,
	debit int NOT NULL,
	PRIMARY KEY (facility_id, id),						-- Added composite PRIMARY KEY
	FOREIGN KEY (facility_id, account_id)				-- Added composite FOREIGN KEY to accounts
		REFERENCES accounts(facility_id, id),
	FOREIGN KEY (facility_id, transaction_id)			-- Added composite FOREIGN KEY to transactions
		REFERENCES transactions(facility_id, id)
);

select create_distributed_table('facilities', 'id');
select create_distributed_table('accounts', 'facility_id');
select create_distributed_table('transactions', 'facility_id');
select create_distributed_table('entries', 'facility_id');