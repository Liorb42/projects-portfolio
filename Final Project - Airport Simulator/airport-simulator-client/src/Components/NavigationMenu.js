import React from 'react';
import { Link } from 'react-router-dom';

function NavigationMenu(props) {
  return (
    <div>
      <div className="font-bold py-3">Airport Simulator</div>
      <ul>
        <li>
          <Link
            to="/"
            className="text-blue-500 py-3 border-t border-b block"
            onClick={props.closeMenu}
          >
            Home
          </Link>
        </li>
        <li>
          <Link
            to="/airport"
            className="text-blue-500 py-3 border-b block"
            onClick={props.closeMenu}
          >
            Airport display
          </Link>
        </li>
      </ul>
    </div>
  );
}

export default NavigationMenu;
