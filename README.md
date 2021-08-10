# PHẦN MỀM QUẢN LÝ KHÁCH SẠN
Phần mềm được code bằng ngôn ngữ lập trình C#, thiết kế bằng Window Form và sử dụng Crytal Report để làm báo cáo
* Có các chức năng chính như:
1. Thuê phòng:
- Cho phép một người có thể thuê được nhiều phòng, mỗi phòng chỉ cần thông tin 1 người, yêu cầu nhập vào số CMND/CC, hộ tên, địa chỉ,...
- Có thể lựa chọn loại phòng mình muốn thuê: Vip, thường, giường đơn, giường đôi
2. Sử dụng dịch vụ
- Dịch vụ sẽ được đi theo phòng, 1 phòng có thể gọi nhiều dịch vụ khác nhau
- Thanh toán dịch vụ sẽ được tính cụ thể lúc trả phòng
3. Trả phòng
- Mỗi lần trả phòng chỉ có thể trả cho 1 phòng
- Ấn thanh toán để nhìn thấy chi tiết hóa đơn
4. Một số chức năng phụ khác
- Tại mục hệ thống -> thông tin có thể thay đổi tên khách sạn, logo và các thông tin khác
** Hướng dẫn cài đặt
1. Đầu tiên hãy mở phần database và chạy query trong SQL Server
2. Khi tải về mọi người hãy đổi phần Connection String trong mỗi form để phù hợp với từng máy (cách đổi thì mọi người tự tìm hiểu thêm nhe)
3. Có thể sẽ có phần thông báo của Guna khi mở form bằng VSCode, mọi người cứ tắt đi nhé
