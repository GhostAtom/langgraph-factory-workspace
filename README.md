# Red Square Component

This project implements a reusable React component that displays a red square. The component is designed to be part of a larger UI component library.

## Features
- Renders a red square with dimensions of 100x100 pixels by default.
- Responsive design that maintains aspect ratio when resized.
- Easily integrates with other UI components.

## Usage

Import the `RedSquare` component and use it within your React application:

```jsx
import RedSquare from './components/RedSquare';

function App() {
  return (
    <div>
      <h1>Red Square Example</h1>
      <RedSquare />
      <RedSquare size={150} /> {/* Custom size */}
    </div>
  );
}
```

## Development

### Installation
1. Clone the repository.
2. Run `npm install` to install dependencies.

### Running Locally
- Execute `npm start` to start the development server.
- Open `http://localhost:9000` in your browser to view the component.

### Testing
- Run `npm test` to execute tests.

## Deployment
Build the app for production using:

```bash
npm run build
```

This will create a `dist` folder with the compiled code.

## License

MIT License
