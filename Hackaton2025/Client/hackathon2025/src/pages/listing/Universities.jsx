import { Edit3, Trash2, Eye } from "lucide-react";
import React from "react";
import Listing from "../../components/ui/Listing";
import Filters from "../../components/ui/Filters";
import { useNavigate } from "react-router-dom";
import useFilters from "../../hooks/useFilters";

const sampleUniversities = [
  {
    _id: "uni1",
    title: "Massachusetts Institute of Technology",
    photo: "https://randomuser.me/api/portraits/lego/1.jpg",
    address: "77 Massachusetts Ave, Cambridge, MA 02139",
    phoneNumber: "+1 (617) 253-1000",
    faculties: [
      "Engineering",
      "Computer Science",
      "Physics",
      "Mathematics",
      "Business",
    ],
    establishedYear: 1861,
  },
  {
    _id: "uni2",
    title: "Stanford University",
    photo: "https://randomuser.me/api/portraits/lego/2.jpg",
    address: "450 Serra Mall, Stanford, CA 94305",
    phoneNumber: "+1 (650) 723-2300",
    faculties: ["Engineering", "Medicine", "Law", "Education"],
    establishedYear: 1885,
  },
  {
    _id: "uni3",
    title: "Harvard University",
    photo: "https://randomuser.me/api/portraits/lego/3.jpg",
    address: "Cambridge, MA 02138",
    phoneNumber: "+1 (617) 495-1000",
    faculties: [
      "Arts and Sciences",
      "Medicine",
      "Law",
      "Business",
      "Education",
      "Engineering",
    ],
    establishedYear: 1636,
  },
];

const columns = [
  {
    key: "_id",
    header: "University ID",
    render: (value) => (
      <span className="font-medium text-gray-800">{value.slice(0, 10)}...</span>
    ),
  },
  {
    key: "photo",
    header: "Logo",
    render: (value) => (
      <img
        src={value}
        alt="University"
        className="w-14 h-14 rounded-full object-cover border"
      />
    ),
  },
  { key: "title", header: "University Name" },
  { key: "address", header: "Address" },
  { key: "phoneNumber", header: "Phone Number" },
  {
    key: "faculties",
    header: "Faculties",
    render: (faculties) => (
      <div className="flex gap-1 flex-wrap max-w-xs">
        {faculties.slice(0, 3).map((faculty, i) => (
          <span
            key={i}
            className="px-2 py-1 text-xs rounded-full bg-green-50 text-green-700 border border-green-200"
          >
            {faculty}
          </span>
        ))}
        {faculties.length > 3 && (
          <span className="text-xs text-gray-500">
            +{faculties.length - 3} more
          </span>
        )}
      </div>
    ),
  },
  {
    key: "establishedYear",
    header: "Est.",
    render: (year) => <span className="text-gray-600">{year}</span>,
  },
];

export default function Universities() {
  const navigate = useNavigate();

  const data = sampleUniversities;
  const isLoading = false;
  const isError = false;

  // Extract unique faculties (flattened from all universities)
  const allFaculties = data
    .flatMap((uni) => uni.faculties)
    .filter((v, i, a) => a.indexOf(v) === i)
    .sort();

  const { filteredData, searchTerm, setSearchTerm, filters, setFilter } =
    useFilters({
      data: data || [],
      config: {
        searchKeys: ["title", "address", "_id"],
        filterKeys: { faculties: allFaculties },
        customFilter: (row, filters) => {
          // Multi-select faculty filter
          if (filters.faculties && filters.faculties.length > 0) {
            const hasMatchingFaculty = row.faculties.some((f) =>
              filters.faculties.includes(f)
            );
            if (!hasMatchingFaculty) return false;
          }
          return true;
        },
      },
    });

  const actions = [
    {
      icon: <Eye size={18} className="text-blue-600" />,
      onClick: (row) => navigate(`/universities/${row._id}`),
    },
    {
      icon: <Edit3 size={18} className="text-gray-600" />,
      onClick: (row) => alert("Edit " + row.title),
    },
    {
      icon: <Trash2 size={18} className="text-red-500" />,
      onClick: (row) => alert("Delete " + row.title),
    },
  ];

  if (isLoading) return <p>Loading universities...</p>;
  if (isError) return <p>Failed to load universities</p>;

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-semibold">Universities</h2>
        <button
          className="px-4 py-2 rounded-xl bg-blue-600 text-white hover:bg-blue-700"
          onClick={() => navigate(`/universities/addUniversity`)}
        >
          Add University
        </button>
      </div>

      <Filters
        searchTerm={searchTerm}
        setSearchTerm={setSearchTerm}
        filters={filters}
        setFilter={setFilter}
        filterConfig={{
          searchKeys: ["title", "address"],
          filterKeys: { faculties: allFaculties },
          multiSelectKeys: ["faculties"], // Enables checkbox multi-select
        }}
      />

      <Listing columns={columns} data={filteredData} actions={actions} />
    </div>
  );
}
