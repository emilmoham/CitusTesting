# Citus Testing
This repository documents some attempts to learn how to set up and use [Citus](https://www.citusdata.com/). The goal of these tests is to measure the performance tradeoffs experienced when using Citus when compared to single instances of PostgreSQL and MSSQL.

## Setup
### Databases
\[This section is not complete\]Full instructions for setting up the databases:
1. [PostgreSQL](docs/VirtualMachines/PostgreSQL.md)
2. [Citus](docs/VirtualMachines/Citus.md)
3. [MSSQL](docs/VirtualMachines/MSSQL.md)

### K6
For load testing, install [k6](https://k6.io/) according to the instructions found [here](https://grafana.com/docs/k6/latest/set-up/install-k6/).

## Tests and Results
### AsyncAPI Load Test
* This test measusres how many insert requests can be handled over a 60 second period.
* Results for this test can be found [here](results/AsyncAPI/results.csv)
