CREATE table facilities (
	id serial PRIMARY KEY,
	name text NOT NULL
);

CREATE TABLE accounts (
	id serial PRIMARY KEY,
	name text NOT NULL,
	number int NOT NULL,
	TYPE boolean NOT NULL,
	facility_id int NOT NULL REFERENCES facilities(id)
);

CREATE TABLE transactions (
	id bigserial PRIMARY KEY,
	facility_id int NOT NULL REFERENCES facilities(id)
);

CREATE TABLE entries (
	id bigserial PRIMARY KEY,
	facility_id int NOT NULL REFERENCES facilities(id),
	transaction_id bigint NOT NULL REFERENCES transactions(id),
	account_id int NOT NULL REFERENCES accounts(id),
	credit int NOT NULL,
	debit int NOT NULL
);
