const nodemailer = require('nodemailer');

// Configuration for SMTP
const transporter = nodemailer.createTransport({
  service: 'gmail',
  auth: {
    user: process.env.EMAIL_USER,
    pass: process.env.EMAIL_PASS
  }
});

// Send Welcome Email
function sendWelcomeEmail(to) {
  const mailOptions = {
    from: process.env.EMAIL_USER,
    to,
    subject: 'Welcome to Our Service',
    text: 'Thank you for creating an account with us!'
  };
  transporter.sendMail(mailOptions, (error, info) => {
    if (error) {
      return console.log(error);
    }
    console.log('Welcome Email sent: ' + info.response);
  });
}

// Send Password Reset Email
function sendPasswordResetEmail(to, token) {
  const mailOptions = {
    from: process.env.EMAIL_USER,
    to,
    subject: 'Password Reset',
    text: `Please use the following link to reset your password: https://example.com/reset?token=${token}`
  };
  transporter.sendMail(mailOptions, (error, info) => {
    if (error) {
      return console.log(error);
    }
    console.log('Password Reset Email sent: ' + info.response);
  });
}

// Send Important Updates Email
function sendImportantUpdatesEmail(to, message) {
  const mailOptions = {
    from: process.env.EMAIL_USER,
    to,
    subject: 'Important Update',
    text: message
  };
  transporter.sendMail(mailOptions, (error, info) => {
    if (error) {
      return console.log(error);
    }
    console.log('Important Updates Email sent: ' + info.response);
  });
}

module.exports = {
  sendWelcomeEmail,
  sendPasswordResetEmail,
  sendImportantUpdatesEmail
};