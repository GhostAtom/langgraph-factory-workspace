const jsdom = require('jsdom');
const { JSDOM } = jsdom;
const fs = require('fs');

const html = fs.readFileSync('./index.html', 'utf8');
const css = fs.readFileSync('./styles.css', 'utf8');

const dom = new JSDOM(html);
const document = dom.window.document;

function applyStyles() {
    const style = document.createElement('style');
    style.textContent = css;
    document.head.appendChild(style);
}

applyStyles();

describe('Red Text Feature', () => {
    test('paragraph element should have red text', () => {
        const paragraph = document.querySelector('p.red-text');
        expect(window.getComputedStyle(paragraph).color).toBe('red');
    });
    
    test('heading element should have red text', () => {
        const heading = document.querySelector('h1.red-text');
        expect(window.getComputedStyle(heading).color).toBe('red');
    });
});
