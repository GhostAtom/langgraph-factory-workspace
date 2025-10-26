const fs = require('fs');
const path = require('path');
const createFdpFile = require('../src/createFile');

jest.mock('fs');

describe('createFdpFile', () => {
  it('should create a file named "fdp" in specified directory', async () => {
    fs.access.mockImplementation((dir, mode, callback) => callback(null));
    fs.open.mockImplementation((filePath, flags, callback) => callback(null, {}));
    fs.close.mockImplementation((fd, callback) => callback(null));

    const result = await createFdpFile('/test-dir');
    expect(result).toBe('File created successfully');
  });

  it('should return error if directory is not writable', async () => {
    fs.access.mockImplementation((dir, mode, callback) => callback(new Error('error')));

    try {
      await createFdpFile('/test-dir');
    } catch (error) {
      expect(error).toBe('Directory not writable');
    }
  });

  it('should return error if file already exists', async () => {
    fs.access.mockImplementation((dir, mode, callback) => callback(null));
    fs.open.mockImplementation((filePath, flags, callback) => {
      const err = new Error('file exists');
      err.code = 'EEXIST';
      callback(err);
    });

    try {
      await createFdpFile('/test-dir');
    } catch (error) {
      expect(error).toBe('File already exists');
    }
  });
});