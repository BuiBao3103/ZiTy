CREATE TABLE `users` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `username` varchar(50) UNIQUE NOT NULL,
  `password` varchar(60) NOT NULL,
  `avatar` varchar(255) DEFAULT null,
  `is_first_login` bool NOT NULL DEFAULT true,
  `email` varchar(100) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `gender` ENUM ('MALE', 'FEMALE') NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `nation_id` varchar(12) UNIQUE NOT NULL,
  `user_type` ENUM ('RESIDENT', 'ADMIN') NOT NULL,
  `date_of_birth` datetime NOT NULL,
  `is_staying` bool NOT NULL DEFAULT true,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null
);

CREATE TABLE `apartments` (
  `id` varchar(5) PRIMARY KEY NOT NULL,
  `area` float NOT NULL,
  `description` text NOT NULL,
  `floor_number` integer NOT NULL,
  `apartment_number` integer NOT NULL,
  `status` ENUM ('IN_USE', 'EMPTY', 'DISRUPTION') NOT NULL DEFAULT 'EMPTY',
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null
);

CREATE TABLE `relationships` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `role` ENUM ('OWNER', 'USER') NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `user_id` integer NOT NULL,
  `apartment_id` varchar(5) NOT NULL
);

CREATE TABLE `reports` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `content` text NOT NULL,
  `title` varchar(200) NOT NULL,
  `status` ENUM ('PENDING', 'IN_PROGRESS', 'RESOLVED', 'REJECTED') NOT NULL DEFAULT 'PENDING',
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `relationship_id` integer
);

CREATE TABLE `rejection_reasons` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `content` text NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `report_id` integer
);

CREATE TABLE `items` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `image` varchar(255),
  `description` varchar(255) NOT NULL,
  `is_receive` bool NOT NULL DEFAULT true,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `user_id` integer
);

CREATE TABLE `surveys` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `total_questions` integer NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `user_create_id` integer
);

CREATE TABLE `questions` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `content` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `survey_id` integer
);

CREATE TABLE `answers` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `content` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `question_id` integer NOT NULL
);

CREATE TABLE `user_answers` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `answer_id` integer,
  `user_id` integer
);

CREATE TABLE `other_answers` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `content` text(255) NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `question_id` integer,
  `user_id` integer
);

CREATE TABLE `bills` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `monthly` varchar(7) NOT NULL,
  `total_price` float NOT NULL DEFAULT 0,
  `old_water` integer DEFAULT null,
  `new_water` integer DEFAULT null,
  `water_reading_date` datetime DEFAULT null,
  `status` ENUM ('UNPAID', 'PAID', 'OVERDUE') NOT NULL DEFAULT 'UNPAID',
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `relationship_id` integer NOT NULL
);

CREATE TABLE `bill_details` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `price` float NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null,
  `bill_id` integer NOT NULL,
  `service_id` integer NOT NULL
);

CREATE TABLE `services` (
  `id` integer PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(255) NOT NULL,
  `price` float NOT NULL,
  `created_at` datetime NOT NULL,
  `updated_at` datetime DEFAULT null,
  `deleted_at` datetime DEFAULT null
);

CREATE TABLE `settings` (
  `id` integer PRIMARY KEY NOT NULL,
  `current_monthly` varchar(7) NOT NULL,
  `system_status` ENUM ('PREPAYMENT', 'PAYMENT', 'OVERDUE', 'DELINQUENT') NOT NULL,
  `room_price_per_m2` float NOT NULL,
  `water_price_per_m3` float NOT NULL,
  `room_vat` float NOT NULL,
  `water_vat` integer NOT NULL,
  `env_protection_tax` integer NOT NULL
);

ALTER TABLE `relationships` ADD FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

ALTER TABLE `relationships` ADD FOREIGN KEY (`apartment_id`) REFERENCES `apartments` (`id`);

ALTER TABLE `rejection_reasons` ADD FOREIGN KEY (`report_id`) REFERENCES `reports` (`id`);

ALTER TABLE `items` ADD FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

ALTER TABLE `surveys` ADD FOREIGN KEY (`user_create_id`) REFERENCES `users` (`id`);

ALTER TABLE `questions` ADD FOREIGN KEY (`survey_id`) REFERENCES `surveys` (`id`);

ALTER TABLE `answers` ADD FOREIGN KEY (`question_id`) REFERENCES `questions` (`id`);

ALTER TABLE `user_answers` ADD FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

ALTER TABLE `user_answers` ADD FOREIGN KEY (`answer_id`) REFERENCES `answers` (`id`);

ALTER TABLE `other_answers` ADD FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

ALTER TABLE `other_answers` ADD FOREIGN KEY (`question_id`) REFERENCES `questions` (`id`);

ALTER TABLE `bill_details` ADD FOREIGN KEY (`bill_id`) REFERENCES `bills` (`id`);

ALTER TABLE `bill_details` ADD FOREIGN KEY (`service_id`) REFERENCES `services` (`id`);

ALTER TABLE `bills` ADD FOREIGN KEY (`relationship_id`) REFERENCES `relationships` (`id`);

ALTER TABLE `reports` ADD FOREIGN KEY (`relationship_id`) REFERENCES `relationships` (`id`);
