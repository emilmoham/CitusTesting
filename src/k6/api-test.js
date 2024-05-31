import http from 'k6/http';
import { check } from "k6";
import { randomIntBetween  } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    vus: 10,
    duration: '60s'
};

export default function () {
    const randomFacilityId = randomIntBetween(1,10);
    const randomAmount = randomIntBetween(1,1000);
    const randomCreditAccount = randomIntBetween(1,5); // TODO Fix: These two account IDs could be the same 
    const randomDebitAccount = randomIntBetween(1,5); // TODO Fix: These two account IDs could be the same
    
    const params = {
        headers: {
            'Content-Type': 'application/json'
        },
    };

    const transactionPayload = JSON.stringify({
        "facilityId": randomFacilityId
    });
    
    const transactionRes = http.post('https://localhost:5001/api/transactions', transactionPayload, params);
    check(transactionRes, {
        'transaction response code was 201': (res) => res.status == 201
    });

    const transactionId = JSON.parse(transactionRes.body).id;
    
    const creditEntryPayload = JSON.stringify({
        "facilityId": randomFacilityId,
        "transactionId": transactionId,
        "accountId": (5 * (randomFacilityId - 1)) + randomCreditAccount,
        "credit": randomAmount,
        "debit": 0
    });

    const creditEntryRes = http.post('https://localhost:5001/api/entries', creditEntryPayload, params);
    check(creditEntryRes, {
        'credit entry response code was 201': (res) => res.status == 201
    });

    const debitEntryPayload = JSON.stringify({
        "facilityId": randomFacilityId,
        "transactionId": transactionId,
        "accountId": (5 * (randomFacilityId - 1)) + randomDebitAccount,
        "credit": randomAmount,
        "debit": randomAmount
    });

    const debitEntryRes = http.post('https://localhost:5001/api/entries', debitEntryPayload, params);
    check(debitEntryRes, {
        'debit entry response code was 201': (res) => res.status == 201
    });
}