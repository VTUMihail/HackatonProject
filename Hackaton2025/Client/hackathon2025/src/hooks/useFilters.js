// hooks/useFilters.js
import { useState, useMemo } from "react";

export default function useFilters({ data, config }) {
  // config: { searchKeys: [], filterKeys: {}, customFilter?: fn }
  const [searchTerm, setSearchTerm] = useState("");
  const [filters, setFilters] = useState({});

  const setFilter = (key, value) => {
    setFilters((prev) => ({ ...prev, [key]: value }));
  };

  const filteredData = useMemo(() => {
    return data.filter((row) => {
      if (searchTerm) {
        const searchMatch = config.searchKeys.some((key) =>
          row[key]?.toLowerCase().includes(searchTerm.toLowerCase())
        );
        if (!searchMatch) return false;
      }

      for (const [key] of Object.entries(config.filterKeys)) {
        const selectedValue = filters[key];
        if (selectedValue && row[key] !== selectedValue) return false;
      }
      if (config.customFilter && !config.customFilter(row, filters)) {
        return false;
      }

      return true;
    });
  }, [data, searchTerm, filters, config]);

  return { filteredData, searchTerm, setSearchTerm, filters, setFilter };
}
