CREATE TABLE facilities (
	id SERIAL PRIMARY KEY,
	name TEXT NOT NULL
);

create table accounts (
	id SERIAL NOT NULL,									-- Removed PRIMARY KEY
	name TEXT NOT NULL,
	number INT NOT NULL,
	type BIT NOT NULL,
	facility_id INT NOT NULL REFERENCES facilities(id),
	PRIMARY KEY (facility_id, id)						-- Added composite PRIMARY KEY
);

create table transactions (
	id BIGSERIAL NOT NULL,								-- Removed PRIMARY KEY
	facility_id INT NOT NULL REFERENCES facilities(id),
	PRIMARY KEY (facility_id, id)						-- Added composite PRIMARY KEY
);


CREATE TABLE entries (
	id BIGSERIAL NOT NULL,								-- Removed PRIMARY KEY
	transaction_id BIGINT NOT NULL ,					-- Removed FOREIGN KEY 
	facility_id INT NOT NULL REFERENCES facilitiees(id),-- Added reference to facility table
	account_id INT NOT NULL,							-- Removed FOREIGN KEY 
	credit INT NOT NULL,
	debit INT NOT NULL,
	PRIMARY KEY (facility_id, id),						-- Added composite PRIMARY KEY
	FOREIGN KEY (facility_id, account_id)				-- Added composite FOREIGN KEY to accounts
		REFERENCES accounts(facility_id, id),
	FOREIGN KEY (facility_id, transaction_id)			-- Added composite FOREIGN KEY to transactions
		REFERENCES transactions(facility_id, id)
);