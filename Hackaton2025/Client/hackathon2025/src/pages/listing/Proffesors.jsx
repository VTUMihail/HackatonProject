import { Edit3, Trash2, Eye } from "lucide-react";
import React from "react";
import Listing from "../../components/ui/Listing";
import Filters from "../../components/ui/Filters";
import { useNavigate } from "react-router-dom";
import useFilters from "../../hooks/useFilters";

// Sample data for testing
const sampleProfessors = [
  {
    _id: "prof1",
    fullName: "Dr. John Smith",
    photo: "https://randomuser.me/api/portraits/men/1.jpg",
    address: "123 University Ave, Boston, MA",
    phoneNumber: "+1 (555) 123-4567",
    faculty: "Math and IT",
    yearsOfWork: 15,
    specialties: ["Artificial Intelligence", "Data Science", "Cryptography"],
  },
  {
    _id: "prof2",
    fullName: "Dr. Emily Johnson",
    photo: "https://randomuser.me/api/portraits/women/2.jpg",
    address: "456 College St, Cambridge, MA",
    phoneNumber: "+1 (555) 987-6543",
    faculty: "Physics",
    yearsOfWork: 8,
    specialties: ["Quantum Mechanics", "Astrophysics"],
  },
  {
    _id: "prof3",
    fullName: "Dr. Michael Brown",
    photo: "https://randomuser.me/api/portraits/men/3.jpg",
    address: "789 Academic Rd, New York, NY",
    phoneNumber: "+1 (555) 456-7890",
    faculty: "Math and IT",
    yearsOfWork: 20,
    specialties: ["Software Engineering", "Algorithms"],
  },
];

const columns = [
  {
    key: "_id",
    header: "Professor ID",
    render: (value) => (
      <span className="font-medium text-gray-800">{value.slice(0, 10)}...</span>
    ),
  },
  {
    key: "photo",
    header: "Photo",
    render: (value) => (
      <img
        src={value}
        alt="Professor"
        className="w-14 h-14 rounded-full object-cover"
      />
    ),
  },
  { key: "fullName", header: "Full Name" },
  { key: "address", header: "Address" },
  { key: "phoneNumber", header: "Phone Number" },
  { key: "faculty", header: "Faculty" },
  {
    key: "yearsOfWork",
    header: "Years of Work",
    render: (val) => <span className="text-gray-600">{val} years</span>,
  },
  {
    key: "specialties",
    header: "Specialties",
    render: (specialties) => (
      <div className="flex gap-1 flex-wrap max-w-xs">
        {specialties.slice(0, 3).map((specialty, i) => (
          <span
            key={i}
            className="px-2 py-1 text-xs rounded-full bg-blue-50 text-blue-600"
          >
            {specialty}
          </span>
        ))}
        {specialties.length > 3 && (
          <span className="text-xs text-gray-500">
            +{specialties.length - 3} more
          </span>
        )}
      </div>
    ),
  },
];

export default function Professors() {
  const navigate = useNavigate();

  // Using sample data instead of useCampsites hook for testing
  const data = sampleProfessors;
  const isLoading = false;
  const isError = false;

  const uniqueFaculties = [...new Set(data?.map((row) => row.faculty))].sort();

  const { filteredData, searchTerm, setSearchTerm, filters, setFilter } =
    useFilters({
      data: data || [],
      config: {
        searchKeys: ["fullName", "address", "_id"],
        filterKeys: { faculty: uniqueFaculties },
        customFilter: (row, filters) => {
          const years = row.yearsOfWork || 0;
          if (filters.minYears && years < parseFloat(filters.minYears))
            return false;
          if (filters.maxYears && years > parseFloat(filters.maxYears))
            return false;
          return true;
        },
      },
    });

  const actions = [
    {
      icon: <Eye size={18} className="text-blue-600" />,
      onClick: (row) => navigate(`/professors/${row._id}`),
    },
    {
      icon: <Edit3 size={18} className="text-gray-600" />,
      onClick: (row) => alert("Edit " + row.fullName),
    },
    {
      icon: <Trash2 size={18} className="text-red-500" />,
      onClick: (row) => alert("Delete " + row.fullName),
    },
  ];

  if (isLoading) return <p>Loading professors...</p>;
  if (isError) return <p>Failed to load professors</p>;

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-semibold">Professors</h2>
        <button
          className="px-4 py-2 rounded-xl bg-blue-600 text-white hover:bg-blue-700"
          onClick={() => navigate(`/professors/addProfessor`)}
        >
          Add Professor
        </button>
      </div>

      <Filters
        searchTerm={searchTerm}
        setSearchTerm={setSearchTerm}
        filters={{
          ...filters,
          minYears: filters.minYears || "",
          maxYears: filters.maxYears || "",
        }}
        setFilter={setFilter}
        filterConfig={{
          searchKeys: ["fullName", "address"],
          filterKeys: { faculty: uniqueFaculties },
        }}
      />

      <Listing columns={columns} data={filteredData} actions={actions} />
    </div>
  );
}
