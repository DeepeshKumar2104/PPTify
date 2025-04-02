-- User Credentials
INSERT INTO user_credentials (user_id, password_hash)
VALUES
('11111111-1111-1111-1111-111111111111', 'hashed_password_1'),
('22222222-2222-2222-2222-222222222222', 'hashed_password_2'),
('33333333-3333-3333-3333-333333333333', 'hashed_password_3'),
('44444444-4444-4444-4444-444444444444', 'hashed_password_4'),
('55555555-5555-5555-5555-555555555555', 'hashed_password_5');

-- Users
INSERT INTO users (user_id, email, full_name, role, phone_number, is_email_verified)
VALUES
('11111111-1111-1111-1111-111111111111', 'alice@example.com', 'Alice Johnson', 'admin', '1234567890', TRUE),
('22222222-2222-2222-2222-222222222222', 'bob@example.com', 'Bob Smith', 'user', '2345678901', TRUE),
('33333333-3333-3333-3333-333333333333', 'carol@example.com', 'Carol Davis', 'user', '3456789012', FALSE),
('44444444-4444-4444-4444-444444444444', 'dan@example.com', 'Dan Brown', 'user', '4567890123', TRUE),
('55555555-5555-5555-5555-555555555555', 'eva@example.com', 'Eva Green', 'admin', '5678901234', TRUE);

-- User Preferences
INSERT INTO user_preferences (preference_id, user_id, theme, language)
VALUES
(UUID(), '11111111-1111-1111-1111-111111111111', 'dark', 'en'),
(UUID(), '22222222-2222-2222-2222-222222222222', 'light', 'en'),
(UUID(), '33333333-3333-3333-3333-333333333333', 'dark', 'fr'),
(UUID(), '44444444-4444-4444-4444-444444444444', 'light', 'de'),
(UUID(), '55555555-5555-5555-5555-555555555555', 'dark', 'es');

-- Auth Tokens
INSERT INTO auth_tokens (token_id, user_id, token, expires_at)
VALUES
(UUID(), '11111111-1111-1111-1111-111111111111', 'token_1', NOW() + INTERVAL 7 DAY),
(UUID(), '22222222-2222-2222-2222-222222222222', 'token_2', NOW() + INTERVAL 7 DAY),
(UUID(), '33333333-3333-3333-3333-333333333333', 'token_3', NOW() + INTERVAL 7 DAY),
(UUID(), '44444444-4444-4444-4444-444444444444', 'token_4', NOW() + INTERVAL 7 DAY),
(UUID(), '55555555-5555-5555-5555-555555555555', 'token_5', NOW() + INTERVAL 7 DAY);

-- Presentations
INSERT INTO presentations (presentation_id, user_id, file_url, status)
VALUES
('aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa', '11111111-1111-1111-1111-111111111111', 'https://cdn/ppt1.pptx', 'completed'),
('bbbb2222-bbbb-bbbb-bbbb-bbbbbbbbbbbb', '22222222-2222-2222-2222-222222222222', 'https://cdn/ppt2.pptx', 'in_progress'),
('cccc3333-cccc-cccc-cccc-cccccccccccc', '33333333-3333-3333-3333-333333333333', 'https://cdn/ppt3.pptx', 'pending'),
('dddd4444-dddd-dddd-dddd-dddddddddddd', '44444444-4444-4444-4444-444444444444', 'https://cdn/ppt4.pptx', 'failed'),
('eeee5555-eeee-eeee-eeee-eeeeeeeeeeee', '55555555-5555-5555-5555-555555555555', 'https://cdn/ppt5.pptx', 'completed');

-- Tasks
INSERT INTO tasks (task_id, presentation_id, task_status, result, started_at, completed_at, duration)
VALUES
(UUID(), 'aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'completed', 'Success', NOW() - INTERVAL 2 HOUR, NOW(), 120),
(UUID(), 'bbbb2222-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'in_progress', NULL, NOW() - INTERVAL 1 HOUR, NULL, NULL),
(UUID(), 'cccc3333-cccc-cccc-cccc-cccccccccccc', 'queued', NULL, NULL, NULL, NULL),
(UUID(), 'dddd4444-dddd-dddd-dddd-dddddddddddd', 'failed', 'Error: Timeout', NOW() - INTERVAL 3 HOUR, NOW() - INTERVAL 2 HOUR, 60),
(UUID(), 'eeee5555-eeee-eeee-eeee-eeeeeeeeeeee', 'completed', 'OK', NOW() - INTERVAL 4 HOUR, NOW() - INTERVAL 3 HOUR, 60);

-- Audit Logs
INSERT INTO audit_logs (log_id, user_id, action_type, action_details, log_level, source, ip_address, user_agent)
VALUES
(UUID(), '11111111-1111-1111-1111-111111111111', 'login', 'Successful login', 'INFO', 'WEB', '192.168.1.10', 'Mozilla/5.0'),
(UUID(), '22222222-2222-2222-2222-222222222222', 'upload', 'Uploaded presentation', 'INFO', 'WEB', '192.168.1.11', 'Mozilla/5.0'),
(UUID(), '33333333-3333-3333-3333-333333333333', 'download', 'Downloaded report', 'INFO', 'API', '192.168.1.12', 'Postman'),
(UUID(), '44444444-4444-4444-4444-444444444444', 'update', 'Changed preferences', 'WARNING', 'WEB', '192.168.1.13', 'Mozilla/5.0'),
(UUID(), '55555555-5555-5555-5555-555555555555', 'delete', 'Deleted file', 'ERROR', 'API', '192.168.1.14', 'Postman');

-- Presentation History
INSERT INTO presentation_history (history_id, presentation_id, change_type, changed_by, change_details)
VALUES
(UUID(), 'aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'created', '11111111-1111-1111-1111-111111111111', 'Initial upload'),
(UUID(), 'bbbb2222-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'updated', '22222222-2222-2222-2222-222222222222', 'Revised title'),
(UUID(), 'cccc3333-cccc-cccc-cccc-cccccccccccc', 'created', '33333333-3333-3333-3333-333333333333', 'Draft added'),
(UUID(), 'dddd4444-dddd-dddd-dddd-dddddddddddd', 'deleted', '44444444-4444-4444-4444-444444444444', 'Removed duplicate'),
(UUID(), 'eeee5555-eeee-eeee-eeee-eeeeeeeeeeee', 'updated', '55555555-5555-5555-5555-555555555555', 'Version bumped');
