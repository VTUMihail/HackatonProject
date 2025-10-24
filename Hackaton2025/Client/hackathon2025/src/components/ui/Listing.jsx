import { Edit3, Trash2, MoreHorizontal } from "lucide-react";
import Card from "./Card";
import React, { useState, useMemo } from "react";

function RowCard({ row, columns, actions }) {
  return (
    <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-4 space-y-2">
      {columns.map((col) => (
        <div
          key={col.key}
          className="flex items-center justify-between py-1 border-b last:border-b-0 border-gray-100"
        >
          <span className="font-semibold text-sm text-gray-500">
            {col.header}:
          </span>
          <div className="text-gray-700">
            {col.render ? col.render(row[col.key], row) : row[col.key]}
          </div>
        </div>
      ))}
      {actions && (
        <div className="flex justify-end pt-2 gap-2">
          {actions.map((action, i) => (
            <button
              key={i}
              onClick={() => action.onClick(row)}
              className="p-2 hover:bg-gray-100 rounded-lg transition"
            >
              {action.icon}
            </button>
          ))}
        </div>
      )}
    </div>
  );
}

export default function Listing({ columns, data, actions }) {
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 15;

  // Calculate paginated data
  const totalPages = Math.ceil(data.length / pageSize);
  const paginatedData = useMemo(() => {
    const start = (currentPage - 1) * pageSize;
    return data.slice(start, start + pageSize);
  }, [currentPage, data]);

  return (
    <div className="space-y-6">
      {/* Table View for Desktop */}
      <div className="hidden md:block">
        <Card className="p-0 overflow-hidden bg-white shadow-lg rounded-2xl border border-gray-100">
          <table className="w-full border-collapse">
            <thead>
              <tr className="bg-gray-50 text-left text-sm font-semibold text-gray-600">
                {columns.map((col) => (
                  <th key={col.key} className="px-6 py-4">
                    {col.header}
                  </th>
                ))}
                {actions && <th className="px-6 py-4 text-right">Actions</th>}
              </tr>
            </thead>
            <tbody className="divide-y divide-gray-200">
              {paginatedData.map((row, idx) => (
                <tr
                  key={idx}
                  className="hover:bg-gray-50 transition-colors duration-200"
                >
                  {columns.map((col) => (
                    <td
                      key={col.key}
                      className="px-6 py-4 text-gray-800 font-medium"
                    >
                      {col.render
                        ? col.render(row[col.key], row)
                        : row[col.key]}
                    </td>
                  ))}
                  {actions && (
                    <td className="px-6 py-4 text-right flex justify-end gap-3">
                      {actions.map((action, i) => (
                        <button
                          key={i}
                          onClick={() => action.onClick(row)}
                          className="p-2.5 text-gray-600 hover:text-blue-600 hover:bg-blue-50 rounded-full transition-colors duration-200 hover:cursor-pointer"
                        >
                          {action.icon}
                        </button>
                      ))}
                    </td>
                  )}
                </tr>
              ))}
            </tbody>
          </table>
        </Card>
      </div>

      {/* Card View for Mobile */}
      <div className="block md:hidden space-y-6">
        {paginatedData.map((row, idx) => (
          <RowCard key={idx} row={row} columns={columns} actions={actions} />
        ))}
      </div>

      {/* Pagination Controls */}
      {totalPages > 1 && (
        <div className="flex items-center justify-center gap-2">
          <button
            onClick={() => setCurrentPage((p) => Math.max(p - 1, 1))}
            disabled={currentPage === 1}
            className="px-3 py-1 text-sm rounded-md border disabled:opacity-50 disabled:cursor-not-allowed hover:bg-gray-100"
          >
            Prev
          </button>
          {Array.from({ length: totalPages }).map((_, i) => (
            <button
              key={i}
              onClick={() => setCurrentPage(i + 1)}
              className={`px-3 py-1 text-sm rounded-md border ${
                currentPage === i + 1
                  ? "bg-blue-500 text-white"
                  : "hover:bg-gray-100"
              }`}
            >
              {i + 1}
            </button>
          ))}
          <button
            onClick={() => setCurrentPage((p) => Math.min(p + 1, totalPages))}
            disabled={currentPage === totalPages}
            className="px-3 py-1 text-sm rounded-md border disabled:opacity-50 disabled:cursor-not-allowed hover:bg-gray-100"
          >
            Next
          </button>
        </div>
      )}
    </div>
  );
}
