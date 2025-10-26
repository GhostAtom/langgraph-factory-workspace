const stripe = require('stripe')(process.env.STRIPE_API_KEY);
const paypal = require('paypal-rest-sdk');

// Initialize PayPal
paypal.configure({
  'mode': 'sandbox', // or 'live'
  'client_id': process.env.PAYPAL_CLIENT_ID,
  'client_secret': process.env.PAYPAL_SECRET
});

async function processStripePayment(paymentData) {
  // Implement Stripe payment processing logic
  const paymentIntent = await stripe.paymentIntents.create({
    amount: paymentData.amount,
    currency: 'usd',
    payment_method_types: ['card'],
  });
  return paymentIntent;
}

async function processPayPalPayment(paymentData) {
  // Implement PayPal payment processing logic
  const create_payment_json = {
    intent: 'sale',
    payer: {
      payment_method: 'paypal'
    },
    transactions: [{
      amount: {
        currency: 'USD',
        total: paymentData.amount
      },
      description: 'Payment description'
    }],
    redirect_urls: {
      return_url: 'http://return.url',
      cancel_url: 'http://cancel.url'
    }
  };

  return new Promise((resolve, reject) => {
    paypal.payment.create(create_payment_json, function (error, payment) {
      if (error) {
        reject(error);
      } else {
        resolve(payment);
      }
    });
  });
}

module.exports = { processStripePayment, processPayPalPayment };
