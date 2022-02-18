USE vaccination;

INSERT INTO insurance (insurance_id, last_name, first_name, carrier, group_number, member_id) VALUES (1, "Anderson", "Tony", "BlueCrossBlueShield", 4579543, "U6475484451");
INSERT INTO insurance (insurance_id, last_name, first_name, carrier, group_number, member_id) VALUES (2, "Donaghy", "Hannah", "BlueCrossBlueShield", 4532153, "U6574254865");
INSERT INTO insurance (insurance_id, last_name, first_name, carrier, group_number, member_id) VALUES (3, "Tavis", "Bruce", "Cigna", 3842335, "U3214754867");
INSERT INTO insurance (insurance_id, last_name, first_name, carrier, group_number, member_id) VALUES (4, "Connors", "Kendra", "BlueCrossBlueShield", 7896528, "U6541247845");
INSERT INTO insurance (insurance_id, last_name, first_name, carrier, group_number, member_id) VALUES (5, "Zegt", "Simon", "Cigna", 1567657, "U63541274574");

INSERT INTO state (state_code, name) VALUES ("AL", "Alabama");
INSERT INTO state (state_code, name) VALUES ("AK", "Alaska");
INSERT INTO state (state_code, name) VALUES ("AZ", "Arizona");
INSERT INTO state (state_code, name) VALUES ("AR", "Arkansas");
INSERT INTO state (state_code, name) VALUES ("CA", "California");
INSERT INTO state (state_code, name) VALUES ("CO", "Colorado");
INSERT INTO state (state_code, name) VALUES ("CT", "Connecticut");
INSERT INTO state (state_code, name) VALUES ("DE", "Delaware");
INSERT INTO state (state_code, name) VALUES ("DC", "District of Columbia");
INSERT INTO state (state_code, name) VALUES ("FL", "Florida");
INSERT INTO state (state_code, name) VALUES ("GA", "Georgia");
INSERT INTO state (state_code, name) VALUES ("HI", "Hawaii");
INSERT INTO state (state_code, name) VALUES ("ID", "Idaho");
INSERT INTO state (state_code, name) VALUES ("IL", "Illinois");
INSERT INTO state (state_code, name) VALUES ("IN", "Indiana");
INSERT INTO state (state_code, name) VALUES ("IA", "Iowa");
INSERT INTO state (state_code, name) VALUES ("KS", "Kansas");
INSERT INTO state (state_code, name) VALUES ("KY", "Kentucky");
INSERT INTO state (state_code, name) VALUES ("LA", "Louisiana");
INSERT INTO state (state_code, name) VALUES ("ME", "Maine");
INSERT INTO state (state_code, name) VALUES ("MD", "Maryland");
INSERT INTO state (state_code, name) VALUES ("MA", "Massachusetts");
INSERT INTO state (state_code, name) VALUES ("MI", "Michigan");
INSERT INTO state (state_code, name) VALUES ("MN", "Minnesota");
INSERT INTO state (state_code, name) VALUES ("MS", "Mississippi");
INSERT INTO state (state_code, name) VALUES ("MO", "Missouri");
INSERT INTO state (state_code, name) VALUES ("MT", "Montana");
INSERT INTO state (state_code, name) VALUES ("NE", "Nebraska");
INSERT INTO state (state_code, name) VALUES ("NV", "Nevada");
INSERT INTO state (state_code, name) VALUES ("NH", "New Hampshire");
INSERT INTO state (state_code, name) VALUES ("NJ", "New Jersey");
INSERT INTO state (state_code, name) VALUES ("NM", "New Mexico");
INSERT INTO state (state_code, name) VALUES ("NY", "New York");
INSERT INTO state (state_code, name) VALUES ("NC", "North Carolina");
INSERT INTO state (state_code, name) VALUES ("ND", "North Dakota");
INSERT INTO state (state_code, name) VALUES ("OH", "Ohio");
INSERT INTO state (state_code, name) VALUES ("OK", "Oklahoma");
INSERT INTO state (state_code, name) VALUES ("OR", "Oregon");
INSERT INTO state (state_code, name) VALUES ("PA", "Pennsylvania");
INSERT INTO state (state_code, name) VALUES ("RI", "Rhode Island");
INSERT INTO state (state_code, name) VALUES ("SC", "South Carolina");
INSERT INTO state (state_code, name) VALUES ("SD", "South Dakota");
INSERT INTO state (state_code, name) VALUES ("TN", "Tennessee");
INSERT INTO state (state_code, name) VALUES ("TX", "Texas");
INSERT INTO state (state_code, name) VALUES ("UT", "Utah");
INSERT INTO state (state_code, name) VALUES ("VT", "Vermont");
INSERT INTO state (state_code, name) VALUES ("VA", "Virginia");
INSERT INTO state (state_code, name) VALUES ("WA", "Washington");
INSERT INTO state (state_code, name) VALUES ("WV", "West Virginia");
INSERT INTO state (state_code, name) VALUES ("WI", "Wisconsin");
INSERT INTO state (state_code, name) VALUES ("WY", "Wyoming");

INSERT INTO address (address_id, zip, street, city, state) VALUES (1, "14623", "1 Vaccine Site Rd", "Rochester", "NY");
INSERT INTO address (address_id, zip, street, city, state) VALUES (2, "03063", "12 Adminstration St", "Nashua", "NH");
INSERT INTO address (address_id, zip, street, city, state) VALUES (3, "14623", "12 Street Rd", "Rochester", "NY");
INSERT INTO address (address_id, zip, street, street_line2, city, state) VALUES (4, "14623", "220 Citizen St", "Apt 1120", "Nashua", "NH");

INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (1, "tanderson@fake.notreal", "Anderson", "Tony", 1, "1990-04-12");
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (2, "banderson@fake.notreal", "Anderson", "Barbara", 1, "1991-12-26");
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (3, "han_don@fake.notreal", "Donaghy", "Hannah", 2, "1984-06-04");
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth, address_id) VALUES (4, "bruce_tavis@fake.notreal", "Tavis", "Bruce", 3, "1978-05-12", 3);
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth, phone_number, address_id) VALUES (5, "novak_tavis@fake.notreal", "Tavis", "Novak", 3, "2000-05-12", "5558729582", 3);
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (6, "kc4431@fake.notreal", "Connors", "Kendra", 4, "1994-04-05");
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (7, "simonsaystext@me.instead", "Zegt", "Simon", 5, "1969-01-14");
INSERT INTO citizen (citizen_id, last_name, first_name, date_of_birth, address_id) VALUES (8, "Slomljen", "Hazel Sophia", "1996-06-25", 4);
INSERT INTO citizen (citizen_id, email, last_name, first_name, date_of_birth) VALUES (9, "khiggs207@fake.notreal", "Higgins", "Kyle", "1998-02-25");

INSERT INTO staff (staff_id, email, username, password, last_name, first_name, token) VALUES (1, "admin@staff.email", "superadmin", "$argon2id$v=19$m=16,t=2,p=1$MTIzNDU2Nzg$Dhk8fwnes+f9vzOwgdALlA", "admin", "super", "vqaB/WFKQpb+neW3OY+UktHWL6jPHbB8VCmVsoIut7k=");
INSERT INTO staff (staff_id, email, username, password, last_name, first_name) VALUES (2, "js@staff.email", "js", "$argon2id$v=19$m=16,t=2,p=1$MTIzNDU2Nzg$Dhk8fwnes+f9vzOwgdALlA", "Smith", "John");
INSERT INTO staff (staff_id, email, username, password, last_name, first_name) VALUES (3, "sb@staff.email", "sb", "$argon2id$v=19$m=16,t=2,p=1$MTIzNDU2Nzg$Dhk8fwnes+f9vzOwgdALlA", "Baker", "Sally");
INSERT INTO staff (staff_id, email, username, password, last_name, first_name) VALUES (4, "vi@staff.email", "vi", "$argon2id$v=19$m=16,t=2,p=1$MTIzNDU2Nzg$Dhk8fwnes+f9vzOwgdALlA", "Irwin", "Vince");

INSERT INTO role (role_id, name) VALUES (1, "Super Admin");
INSERT INTO role (role_id, name) VALUES (2, "Site Admin");
INSERT INTO role (role_id, name) VALUES (3, "Vaccine Administration");
INSERT INTO role (role_id, name) VALUES (4, "Check In");

INSERT INTO staff_role (staff_id, role_id, granted_by) VALUES (1, 1, 1);
INSERT INTO staff_role (staff_id, role_id, granted_by) VALUES (2, 2, 1);
INSERT INTO staff_role (staff_id, role_id, granted_by) VALUES (3, 3, 2);
INSERT INTO staff_role (staff_id, role_id, granted_by) VALUES (4, 4, 2);

INSERT INTO location (location_id, name, address_id) VALUES (1, "Rochester Distribution Center", 1);
INSERT INTO location (location_id, name, address_id) VALUES (2, "Wildly out of state second location", 2);

INSERT INTO staff_location(staff_id, location_id) VALUES (1, 1);
INSERT INTO staff_location(staff_id, location_id) VALUES (1, 2);
INSERT INTO staff_location(staff_id, location_id) VALUES (2, 1);
INSERT INTO staff_location(staff_id, location_id) VALUES (3, 1);
INSERT INTO staff_location(staff_id, location_id) VALUES (4, 1);

INSERT INTO vaccine_category (category_id, name) VALUES (1, "Single Dose");
INSERT INTO vaccine_category (category_id, name) VALUES (2, "1st Dose");
INSERT INTO vaccine_category (category_id, name) VALUES (3, "2nd Dose");
INSERT INTO vaccine_category (category_id, name) VALUES (4, "Booster");

INSERT INTO vaccine_disease (disease_id, name) VALUES (1, "COVID-19");

INSERT INTO vaccine (vaccine_id, category, disease, description) VALUES (1, 2, 1, "Covid first dose");
INSERT INTO vaccine (vaccine_id, category, disease, description) VALUES (2, 3, 1, "Covid second dose, recommended 2 weeks after first dose");
INSERT INTO vaccine (vaccine_id, category, disease, description) VALUES (3, 4, 1, "Covid booster, recommended 6 months after second dose");

INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (1, "Pfizer", 1, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (2, "Pfizer", 1, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (3, "Pfizer", 1, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (4, "Pfizer", 1, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (5, "Pfizer", 2, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (6, "Pfizer", 2, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (7, "Pfizer", 2, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (8, "Pfizer", 2, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (9, "Pfizer", 3, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (10, "Pfizer", 3, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (11, "Pfizer", 3, 1);
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id) VALUES (12, "Pfizer", 3, 1);

INSERT INTO timeslot_status (status_id, description) VALUES (1, "AVAILABLE");
INSERT INTO timeslot_status (status_id, description) VALUES (2, "RESERVED");
INSERT INTO timeslot_status (status_id, description) VALUES (3, "FINISHED");
INSERT INTO timeslot_status (status_id, description) VALUES (4, "MISSED");

INSERT INTO timeslot (timeslot_id, staff_id, citizen_id, location_id, dose_id, date, status_id) VALUES (1, 3, 1, 1, 1, "2022-08-01 10:00:00", 2);
INSERT INTO timeslot (timeslot_id, staff_id, citizen_id, location_id, dose_id, date, status_id) VALUES (2, 3, 2, 1, 1, "2022-08-01 10:30:00", 2);
INSERT INTO timeslot (timeslot_id, staff_id, citizen_id, location_id, dose_id, date, status_id) VALUES (3, 3, 3, 1, 1, "2022-08-01 11:00:00", 2);
INSERT INTO timeslot (timeslot_id, staff_id, location_id, dose_id, date, status_id) VALUES (4, 3, 1, 1, "2022-08-01 10:00:00", 1);
INSERT INTO timeslot (timeslot_id, staff_id, citizen_id, location_id, dose_id, date, status_id) VALUES (5, 3, 4, 1, 1, "2022-02-01 10:00:00", 3);
