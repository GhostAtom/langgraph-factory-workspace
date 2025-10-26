const crypto = require('crypto');
const nodemailer = require('nodemailer');
const User = require('../models/User');

exports.initiatePasswordReset = async (req, res) => {
  try {
    const { email } = req.body;
    const user = await User.findOne({ email });
    if (!user) return res.status(400).json({ msg: 'User not found' });

    const token = crypto.randomBytes(20).toString('hex');
    // Save token logic, ideally in a password reset collection or field.

    const transporter = nodemailer.createTransport({
      service: 'Gmail',
      auth: {
        user: 'your-email@gmail.com',
        pass: 'your-email-password'
      }
    });

    const mailOptions = {
      to: user.email,
      from: 'passwordreset@demo.com',
      subject: 'Password Reset Request',
      text: `Please reset your password by clicking the following link: http://localhost:5000/reset/${token}`
    };

    transporter.sendMail(mailOptions, (err) => {
      if (err) return res.status(500).json({ error: 'Error sending email' });
      res.json({ msg: 'Email sent' });
    });
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: 'Server error' });
  }
};