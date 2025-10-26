const { sendWelcomeEmail, sendPasswordResetEmail, sendImportantUpdatesEmail } = require('../src/index');
const nodemailer = require('nodemailer');
const sinon = require('sinon');
const { expect } = require('chai');

describe('Email Notification System', () => {
  let sendMailStub;

  beforeEach(() => {
    sendMailStub = sinon.stub(nodemailer.createTransport().constructor.prototype, 'sendMail');
    sendMailStub.yields(null, { response: 'Email sent' });
  });

  afterEach(() => {
    sendMailStub.restore();
  });

  it('should send a welcome email', () => {
    sendWelcomeEmail('test@example.com');
    expect(sendMailStub.calledOnce).to.be.true;
    expect(sendMailStub.args[0][0].subject).to.equal('Welcome to Our Service');
  });

  it('should send a password reset email', () => {
    sendPasswordResetEmail('test@example.com', 'dummyToken');
    expect(sendMailStub.calledOnce).to.be.true;
    expect(sendMailStub.args[0][0].subject).to.equal('Password Reset');
  });

  it('should send an important updates email', () => {
    sendImportantUpdatesEmail('test@example.com', 'Important update!');
    expect(sendMailStub.calledOnce).to.be.true;
    expect(sendMailStub.args[0][0].subject).to.equal('Important Update');
  });
});