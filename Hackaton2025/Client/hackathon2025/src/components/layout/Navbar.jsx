import { Menu } from "lucide-react";
import React from "react";
export default function Navbar({ onMenuClick }) {
  return (
    <header className="h-16 bg-white border-b border-gray-200 flex items-center justify-between px-4">
      <button onClick={onMenuClick} className="lg:hidden text-gray-600">
        <Menu size={24} />
      </button>

    </header>
  );
}
