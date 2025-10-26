const request = require('supertest');
const mongoose = require('mongoose');
const app = require('../src/server');
const Config = require('../src/models/Config');

beforeAll(async () => {
    await mongoose.connect('mongodb://localhost:27017/test', {
        useNewUrlParser: true,
        useUnifiedTopology: true
    });
});

afterAll(async () => {
    await mongoose.connection.close();
});

describe('DELETE /config/:id', () => {
    let configId;
    let token = 'valid-jwt-token'; // Mock a valid JWT token for tests

    beforeEach(async () => {
        const config = new Config({ name: 'Test Config', value: 'Test Value' });
        await config.save();
        configId = config._id;
    });

    afterEach(async () => {
        await Config.deleteMany();
    });

    it('should delete an existing configuration', async () => {
        const res = await request(app)
            .delete(`/config/${configId}`)
            .set('Authorization', `Bearer ${token}`);
        expect(res.statusCode).toEqual(200);
        expect(res.text).toEqual('Configuration deleted successfully');
    });

    it('should return 404 for non-existent configuration', async () => {
        const res = await request(app)
            .delete('/config/60d9f11421b38c9ef14de254')
            .set('Authorization', `Bearer ${token}`);
        expect(res.statusCode).toEqual(404);
        expect(res.text).toEqual('Configuration does not exist');
    });

    it('should return 401 for unauthorized request', async () => {
        const res = await request(app)
            .delete(`/config/${configId}`);
        expect(res.statusCode).toEqual(401);
    });
});
