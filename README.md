# Citus Testing
This repository documents some attempts to learn how to set up and use [Citus](https://www.citusdata.com/).
The goal of these tests is to measure the performance tradeoffs experienced when
 using Citus when compared to single instances of PostgreSQL and MSSQL.
 
## Setup
### Databases
\[These sections are not complete\]

Instructions for setting up the databases:
1. [PostgreSQL](docs/VirtualMachines/PostgreSQL.md)
2. [Citus](docs/VirtualMachines/Citus.md)
3. [MSSQL](docs/VirtualMachines/MSSQL.md)

### K6
For load testing, install [k6](https://k6.io/) according to the instructions 
found [here](https://grafana.com/docs/k6/latest/set-up/install-k6/).

## Tests and Results
### AsyncAPI Load Test
* This test measusres how many insert requests can be handled over a 60 second
  period.
* Running this test:
  * Run the AsyncAPI project in the CitusTesting solution. Choose the
    configuration that matches the running backend.
  * Run the load test script with `k6 run src\k6\api-test.js`
* A CSV file of the results for this test can be found [here](results/AsyncAPI/results.csv)
