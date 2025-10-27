# Button Component

This component is a reusable button designed to be used across the application. It adheres to the design specifications provided by the UI/UX team.

## Features
- Customizable size: small, medium, large
- Customizable color: default, primary, secondary
- States: hover, active, disabled

## Usage
```jsx
import Button from './components/Button';

function App() {
  return (
    <Button onClick={() => console.log('Button clicked!')} color="primary">Click Me</Button>
  );
}
```

## Development
To run the tests for this component, use:
```bash
npm test
```

## Accessibility
This component is designed to be accessible and meets WCAG standards.

## License
MIT
