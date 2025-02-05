using PhoneNumbers;

namespace Mc2.CrudTest.Tests;

public class PhoneNumberTests
{
    
    [Theory]
    [InlineData("+989121234567", true)]
    [InlineData("+982188776655", false)]
    public void Should_Validate_PhoneNumber_Correctly(string phoneNumber, bool expectedIsMobile)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
        var number = phoneUtil.Parse(phoneNumber, null);
        var isValidMobile = phoneUtil.IsValidNumber(number) 
                            && phoneUtil.GetNumberType(number) == PhoneNumberType.MOBILE;
        
        Assert.Equal(expectedIsMobile, isValidMobile);
    }
}