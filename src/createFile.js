const fs = require('fs');
const path = require('path');

function createFdpFile(directory) {
  return new Promise((resolve, reject) => {
    const filePath = path.join(directory, 'fdp');
    fs.access(directory, fs.constants.W_OK, (err) => {
      if (err) {
        return reject('Directory not writable');
      }
      fs.open(filePath, 'wx', (err, fd) => {
        if (err) {
          if (err.code === 'EEXIST') {
            return reject('File already exists');
          }
          return reject('Error creating file');
        }
        fs.close(fd, (err) => {
          if (err) {
            return reject('Error closing file');
          }
          resolve('File created successfully');
        });
      });
    });
  });
}

module.exports = createFdpFile;