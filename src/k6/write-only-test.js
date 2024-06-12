import http from 'k6/http';
import { check, sleep } from 'k6';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    stages: [
        { duration: '1m', target: 100 },
        { duration: '3m', target: 100 },
        { duration: '1m', target: 0 }
    ],
};

export default function () {
    const randomFacilityName = randomString(16);

    const params = {
        headers: {
            'Content-Type' : 'application/json'
        },
    };

    const createFacilityPayload = JSON.stringify({
        'name': randomFacilityName
    });

    const createFacilityResponse = http.post('http://localhost:5000/api/facilities', createFacilityPayload, params);
    check(createFacilityResponse, {
        'create facility resposne code was 201': (res) => res.status == 201
    });

    sleep(1);
}