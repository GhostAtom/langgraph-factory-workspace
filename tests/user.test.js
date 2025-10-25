const request = require('supertest');
const app = require('../src/index');
const mongoose = require('mongoose');
const User = require('../src/models/User');

beforeAll(async () => {
  await mongoose.connect('mongodb://localhost:27017/test', {
    useNewUrlParser: true,
    useUnifiedTopology: true
  });
});

afterAll(async () => {
  await mongoose.connection.close();
});

describe('User Status API', () => {
  let user;
  beforeEach(async () => {
    await User.deleteMany();
    user = await User.create({ name: 'John Doe', email: 'john@example.com', status: 'active' });
  });

  test('retrieve status of an active user', async () => {
    const response = await request(app)
      .get(`/api/users/${user._id}/status`)
      .expect(200);
    expect(response.body.status).toBe('active');
  });

  test('retrieve status of a non-existent user', async () => {
    const response = await request(app)
      .get(`/api/users/60d0fe4f5311236168a109ca/status`)
      .expect(404);
  });
});