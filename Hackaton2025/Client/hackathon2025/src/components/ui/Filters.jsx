import React from "react";

export default function Filters({
  searchTerm,
  setSearchTerm,
  filters,
  setFilter,
  filterConfig,
}) {
  // Helper to clear a filter
  const removeFilter = (key, value = "") => {
    setFilter(key, value);
  };

  // Generate active filters
  const activeFilters = Object.entries(filters).filter(([value]) => {
    if (!value) return false;
    if (Array.isArray(value) && value.length === 0) return false;
    return true;
  });

  return (
    <div className="flex flex-col gap-2">
      {/* Top row: search + dropdowns + ranges */}
      <div className="flex gap-4 flex-wrap">
        {/* 🔎 Global Search */}
        {filterConfig.searchKeys?.length > 0 && (
          <input
            type="text"
            placeholder="Search..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="px-4 py-2 border rounded-lg flex-1 min-w-[200px]"
          />
        )}

        {/* 🏷️ Dropdowns */}
        {filterConfig.filterKeys &&
          Object.entries(filterConfig.filterKeys).map(([key, options]) => (
            <select
              key={key}
              value={filters[key] || ""}
              onChange={(e) => setFilter(key, e.target.value)}
              className="px-4 py-2 border rounded-lg min-w-[150px]"
            >
              <option value="">All {key}</option>
              {options.map((opt) => (
                <option key={opt} value={opt}>
                  {opt}
                </option>
              ))}
            </select>
          ))}

        {/* Example: min/max range inputs */}
        {"minPrice" in filters && (
          <>
            <input
              type="number"
              placeholder="Min Price"
              value={filters.minPrice || ""}
              onChange={(e) => setFilter("minPrice", e.target.value)}
              className="px-4 py-2 border rounded-lg w-[120px]"
            />
            <input
              type="number"
              placeholder="Max Price"
              value={filters.maxPrice || ""}
              onChange={(e) => setFilter("maxPrice", e.target.value)}
              className="px-4 py-2 border rounded-lg w-[120px]"
            />
          </>
        )}
      </div>

      {/* Bottom row: active filters as tags */}
      {activeFilters.length > 0 && (
        <div className="flex flex-wrap gap-2 mt-2">
          {activeFilters.map(([key, value]) => {
            // For array filters (like multi-select tags), show each separately
            if (Array.isArray(value)) {
              return value.map((v) => (
                <span
                  key={`${key}-${v}`}
                  className="flex items-center gap-1 px-2 py-1 bg-gray-200 rounded-full text-sm cursor-pointer"
                  onClick={() =>
                    removeFilter(
                      key,
                      value.filter((val) => val !== v)
                    )
                  }
                >
                  {key}: {v} <span className="font-bold">×</span>
                </span>
              ));
            }
            // Single value filters
            return (
              <span
                key={key}
                className="flex items-center gap-1 px-2 py-1 bg-gray-200 rounded-full text-sm cursor-pointer"
                onClick={() => removeFilter(key, "")}
              >
                {key}: {value} <span className="font-bold">×</span>
              </span>
            );
          })}
        </div>
      )}
    </div>
  );
}
