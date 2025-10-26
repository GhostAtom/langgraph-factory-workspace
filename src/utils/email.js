const nodemailer = require('nodemailer');

const transporter = nodemailer.createTransport({
  service: 'gmail',
  auth: {
    user: process.env.EMAIL_USER,
    pass: process.env.EMAIL_PASS
  }
});

async function sendPasswordResetEmail(email, token) {
  const link = `http://localhost:${process.env.PORT}/auth/update-password?token=${token}`;
  await transporter.sendMail({
    to: email,
    subject: 'Password Reset',
    text: `Click this link to reset your password: ${link}`
  });
}

module.exports = { sendPasswordResetEmail };
