import React from "react";

export default function RecentActivityCard({ activity }) {
  return (
      <li
        key={activity.id}
        className="p-6 flex items-start space-x-4 hover:bg-gray-50 transition hover:cursor-pointer"
      >
        <activity.icon className={`mt-1 ${activity.color}`} size={24} />
        <div className="flex-1">
          <p className="text-gray-800 font-medium">{activity.description}</p>
          <p className="text-sm text-gray-500">{activity.time}</p>
        </div>
      </li>
  );
}
