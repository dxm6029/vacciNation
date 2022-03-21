USE vaccination;

INSERT INTO insurance (insurance_id, name, carrier, group_number, member_id) VALUES (1, "Tony Anderson", "BlueCrossBlueShield", "4579543", "U6475484451");
INSERT INTO insurance (insurance_id, name, carrier, group_number, member_id) VALUES (2, "Hannah Donaghy", "BlueCrossBlueShield", "4532153", "U6574254865");
INSERT INTO insurance (insurance_id, name, carrier, group_number, member_id) VALUES (3, "Bruce Tavis", "Cigna", "3842335", "U3214754867");
INSERT INTO insurance (insurance_id, name, carrier, group_number, member_id) VALUES (4, "Kendra Connors", "BlueCrossBlueShield", "7896528", "U6541247845");
INSERT INTO insurance (insurance_id, name, carrier, group_number, member_id) VALUES (5, "Simon Zegt", "Cigna", "1567657", "U63541274574");
INSERT INTO insurance (insurance_id, name, carrier, member_id, group_number) VALUES (6, "Robert C. Jones", "Excellus BC/BS", "ZGJ8043434", "TQ570-A0");
INSERT INTO insurance (insurance_id, carrier, member_id, group_number) VALUES (7, "Excellus BC/BS", "XZA XZ99999", "Y0831-AC");
INSERT INTO insurance (insurance_id, carrier, member_id, group_number) VALUES (8, "Aetna", "W1234 56789", "123456-101");
INSERT INTO insurance (insurance_id, carrier, member_id) VALUES (9, "Excellus BC/BS", "MSS 5409200");

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
INSERT INTO address (address_id, zip, street, city, state) VALUES (5, "14624", "12 Ames St", "Chili", "NY");
INSERT INTO address (address_id, zip, street, city, state) VALUES (6, "14604", "3200 Main St", "Rochester", "NY");
INSERT INTO address (address_id, zip, street, city, state) VALUES (7, "14614", "450 East Ave", "Rochester", "NY");
INSERT INTO address (address_id, zip, street, city, state) VALUES (8, "14580", "1234 Lake Rd", "Rochester", "NY");
INSERT INTO address (address_id, zip, street, street_line2, city, state) VALUES (9, "14623", "2400 Jefferson Rd", "Apt 3A", "Rochester", "NY");

INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (1, "tanderson@fake.notreal", "Anderson", "Tony", 1, "1990-04-12");
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (2, "banderson@fake.notreal", "Anderson", "Barbara", 1, "1991-12-26");
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (3, "han_don@fake.notreal", "Donaghy", "Hannah", 2, "1984-06-04");
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth, address_id) VALUES (4, "bruce_tavis@fake.notreal", "Tavis", "Bruce", 3, "1978-05-12", 3);
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth, phone_number, address_id) VALUES (5, "novak_tavis@fake.notreal", "Tavis", "Novak", 3, "2000-05-12", "5558729582", 3);
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (6, "kc4431@fake.notreal", "Connors", "Kendra", 4, "1994-04-05");
INSERT INTO citizen (citizen_id, email, last_name, first_name, insurance_id, date_of_birth) VALUES (7, "simonsaystext@me.instead", "Zegt", "Simon", 5, "1969-01-14");
INSERT INTO citizen (citizen_id, last_name, first_name, date_of_birth, address_id, phone_number) VALUES (8, "Slomljen", "Hazel Sophia", "1996-06-25", 4, "5552452454");
INSERT INTO citizen (citizen_id, email, last_name, first_name, date_of_birth) VALUES (9, "khiggs207@fake.notreal", "Higgins", "Kyle", "1998-02-25");
INSERT INTO citizen (citizen_id, first_name, last_name, date_of_birth, email, address_id, insurance_id) VALUES (10, "Amanda", "Jones", "1990-01-01", "sample@yahoo.com", 5, 6);
INSERT INTO citizen (citizen_id, first_name, last_name, date_of_birth, email, address_id, insurance_id) VALUES (11, "Nathan Donald", "Green", "1997-02-01", "sample@aol.com", 6, 7);
INSERT INTO citizen (citizen_id, first_name, last_name, date_of_birth, email, phone_number, address_id, insurance_id) VALUES (12, "Lavonne", "Davis", "2001-12-05", "sample@gmail.com", "5851234567", 7, 8);
INSERT INTO citizen (citizen_id, first_name, last_name, date_of_birth, phone_number, address_id, insurance_id) VALUES (13, "Harold James", "Clark", "1967-03-17", "5852345678", 8, 9);
INSERT INTO citizen (citizen_id, first_name, last_name, date_of_birth, email, address_id) VALUES (14, "Susan", "Lee", "1985-04-10", "sample2@gmail.com", 9);

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
INSERT INTO dose (dose_id, supplier, vaccine_id, location_id, batch) VALUES (12, "Pfizer", 3, 1, "27");

INSERT INTO timeslot_status (status_id, description) VALUES (1, "AVAILABLE");
INSERT INTO timeslot_status (status_id, description) VALUES (2, "RESERVED");
INSERT INTO timeslot_status (status_id, description) VALUES (3, "FINISHED");
INSERT INTO timeslot_status (status_id, description) VALUES (4, "MISSED");

INSERT INTO timeslot (timeslot_id, staff_id, citizen_id, location_id, dose_id, date, status_id) VALUES (1, 3, 1, 1, 1, "2022-08-01 10:00:00", 2);
INSERT INTO timeslot (timeslot_id, staff_id, citizen_id, location_id, dose_id, date, status_id) VALUES (2, 3, 2, 1, 1, "2022-08-01 10:30:00", 2);
INSERT INTO timeslot (timeslot_id, staff_id, citizen_id, location_id, dose_id, date, status_id) VALUES (3, 3, 3, 1, 1, "2022-08-01 11:00:00", 2);
INSERT INTO timeslot (timeslot_id, staff_id, location_id, dose_id, date, status_id) VALUES (4, 3, 1, 1, "2022-08-01 10:00:00", 1);
INSERT INTO timeslot (timeslot_id, staff_id, citizen_id, location_id, dose_id, date, status_id) VALUES (5, 3, 4, 1, 1, "2022-02-01 10:00:00", 3);

INSERT INTO eligibility (eligibility_id, vaccine_id, dependency) VALUES (1, 1, NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (1, "en", 1, "Q", "Are you aged 65 or older?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (1, "es", 1, "Q", "Tiene 65 anos o mas?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (2, "en", 1, "A", "Yes", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (2, "es", 1, "A", "Si", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (3, "en", 1, "A", "No", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (3, "es", 1, "A", "No", NULL);

INSERT INTO eligibility (eligibility_id, vaccine_id, dependency) VALUES (2, 1, 3); -- Dependent on eligibility_text with text_id '3' i.e. this only appears if citizen chooses 'no' to being 65 or over
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (4, "en", 2, "Q", "Are you a healthcare worker or first responder?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (4, "es", 2, "Q", "Eres un trabajador de la salud o socorrista?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (5, "en", 2, "A", "Yes", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (5, "es", 2, "A", "Si", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (6, "en", 2, "A", "No", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (6, "es", 2, "A", "No", NULL);

INSERT INTO eligibility (eligibility_id, vaccine_id, dependency) VALUES (3, 1, 6); -- Dependent on eligibility_text with text_id '6' i.e. not a healthcare worker
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (7, "en", 3, "Q", "Do you have any of the following health conditions?\n\nCancer\nChronic kidney disease\nChronic liver disease\nChronic lung diseases\nDementia or other neurological conditions\nDiabetes\nDown syndrome\nHeart conditions\nHIV infection\nImmunocompromised state (weakened immune system)\nMental health conditions\nOverweight and obesity\nPregnancy\nSickle cell disease or thalassemia\nSmoking, current or former\nSolid organ or blood stem cell transplant\nStroke or cerebrovascular disease, which affects blood flow to the brain\nSubstance use disorders\nTuberculosis", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (7, "es", 3, "Q", "Tiene alguna de las siguientes condiciones de salud?\n\nCancer\nenfermedad renal cronica\nEnfermedad cronica del higado\nenfermedades pulmonares cronicas\nDemencia u otras condiciones neurologicas\nDiabetes\nSindrome de Down\nEnfermedades del corazon\ninfeccion por VIH\nEstado inmunocomprometido (sistema inmunitario debilitado)\n\nCondiciones de salud mental\nSobrepeso y obesidad\nEl embarazo\nEnfermedad de celulas falciformes o talasemia\nFumar, actual o forma\nTrasplante de organos solidos o celulas madre sanguineas\nAccidente cerebrovascular o enfermedad cerebrovascular, que afecta el flujo de sangre al cerebro\nTrastornos por uso de sustancias\nTuberculosis", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (8, "en", 3, "A", "Yes", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (8, "es", 3, "A", "Si", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (9, "en", 3, "A", "No", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (9, "es", 3, "A", "No", NULL);

INSERT INTO eligibility (eligibility_id, vaccine_id, dependency) VALUES (4, 1, 9); -- Dependent on eligibility_text with text_id '9' i.e. no health conditions
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (10, "en", 4, "Q", "How many doses of the COVID 19 vaccine have you had?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (10, "es", 4, "Q", "Cuantas dosis de la vacuna COVID 19 ha recibido?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (11, "en", 4, "A", "0 Zero", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (11, "es", 4, "A", "0 Cero", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (12, "en", 4, "A", "1 One", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (12, "es", 4, "A", "1 Uno", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (13, "en", 4, "A", "2 Two", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (13, "es", 4, "A", "2 Dos", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (14, "en", 4, "A", "3 Three", FALSE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (14, "es", 4, "A", "3 Tres", FALSE);

INSERT INTO eligibility (eligibility_id, vaccine_id, dependency) VALUES (5, 1, 12); -- Dependent on eligibility_text with text_id '12' i.e. one dose so far
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (15, "en", 5, "Q", "Was your last vaccine more than 3 weeks ago?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (15, "es", 5, "Q", "Su ultima vacuna fue hace mas de 3 semanas?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (16, "en", 5, "A", "Yes", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (16, "es", 5, "A", "Si", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (17, "en", 5, "A", "No", FALSE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (17, "es", 5, "A", "No", FALSE);

INSERT INTO eligibility (eligibility_id, vaccine_id, dependency) VALUES (6, 1, 13); -- Dependent on eligibility_text with text_id '13' i.e. two doses so far
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (18, "en", 6, "Q", "Was your last vaccine more than 6 months ago?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (18, "es", 6, "Q", "Su ultima vacuna fue hace mas de 6 meses?", NULL);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (19, "en", 6, "A", "Yes", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (19, "es", 6, "A", "Si", TRUE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (20, "en", 6, "A", "No", FALSE);
INSERT INTO eligibility_text (text_id, language, eligibility_id, type, text, is_eligible) VALUES (20, "es", 6, "A", "No", FALSE);