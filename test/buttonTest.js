const { JSDOM } = require('jsdom');

// Create a DOM environment
const dom = new JSDOM(`<!DOCTYPE html><body><button id="alertButton">Click Me!</button><script src=""></script></body>`);
global.document = dom.window.document;
global.window = dom.window;

describe('Button Tests', () => {
    test('Button renders on the page', () => {
        const button = document.getElementById('alertButton');
        expect(button).not.toBeNull();
    });

    test('Button click should trigger alert', () => {
        const button = document.getElementById('alertButton');
        window.alert = jest.fn();
        button.click();
        expect(window.alert).toHaveBeenCalledWith('Button clicked!');
    });
});