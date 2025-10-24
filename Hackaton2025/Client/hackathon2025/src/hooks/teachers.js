/* eslint-disable no-unused-vars */
// hooks/useTeachers.js
import { useQuery, useQueries } from "@tanstack/react-query";
import { getTeacherById, getTeachers } from "../services/teacherService";

export const useGetTeachers = (config) => {
  return useQuery({
    queryKey: ["fetch-teachers"],
    queryFn: getTeachers,
  });
};
export const useGetTeacherById = (id, config) => {
  return useQuery({
    queryKey: ["fetch-teacher", id],
    queryFn: () => getTeacherById(id),
    enabled: !!id,
  });
};

