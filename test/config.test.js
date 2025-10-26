const request = require('supertest');
const mongoose = require('mongoose');
const jwt = require('jsonwebtoken');
const app = require('../src/server');
const Config = require('../src/models/Config');

const userId = 'testUser';
const token = jwt.sign({ id: userId }, 'your_jwt_secret');

beforeAll(async () => {
  await mongoose.connect('mongodb://localhost:27017/configTestDB', {
    useNewUrlParser: true,
    useUnifiedTopology: true
  });
});

afterAll(async () => {
  await mongoose.connection.close();
});

describe('DELETE /api/configs/:id', () => {
  let config;

  beforeEach(async () => {
    config = new Config({ name: 'Test Config', value: 'Test Value' });
    await config.save();
  });

  afterEach(async () => {
    await Config.deleteMany({});
  });

  test('successfully deletes a configuration', async () => {
    const res = await request(app)
      .delete(`/api/configs/${config._id}`)
      .set('Authorization', token);
    expect(res.statusCode).toEqual(200);
    expect(res.text).toBe('Configuration deleted successfully.');
  });

  test('returns 404 for non-existent configuration', async () => {
    const fakeId = mongoose.Types.ObjectId();
    const res = await request(app)
      .delete(`/api/configs/${fakeId}`)
      .set('Authorization', token);
    expect(res.statusCode).toEqual(404);
    expect(res.text).toBe('Configuration not found.');
  });

  test('denies access without token', async () => {
    const res = await request(app)
      .delete(`/api/configs/${config._id}`);
    expect(res.statusCode).toEqual(403);
    expect(res.text).toBe('Access denied. No token provided.');
  });
});
