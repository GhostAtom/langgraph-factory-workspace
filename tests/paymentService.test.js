const { processStripePayment, processPayPalPayment } = require('../src/services/paymentService');

describe('Payment Service Tests', () => {
  test('Process Stripe Payment', async () => {
    const paymentData = {
      amount: 5000
    };
    const result = await processStripePayment(paymentData);
    expect(result).toHaveProperty('id');
  });

  test('Process PayPal Payment', async () => {
    const paymentData = {
      amount: '50.00'
    };
    const result = await processPayPalPayment(paymentData);
    expect(result).toHaveProperty('id');
  });
});
