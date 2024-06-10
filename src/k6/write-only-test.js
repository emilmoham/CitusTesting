import http from 'k6/http';
import { check } from 'k6';
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js';

export const options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    vus: 10,
    iterations: 100000
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

    const createFacilityResponse = http.post('https://localhost:5001/api/facilities', createFacilityPayload, params);
    check(createFacilityResponse, {
        'create facility resposne code was 201': (res) => res.status == 201
    });
}