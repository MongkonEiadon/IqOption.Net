using FluentAssertions;
using FluentAssertions.Extensions;
using IqOptionApi.Models;
using IqOptionApi.ws.@base;
using NUnit.Framework;

namespace IqOptionApi.Tests.JsonTest.Profiles {
    [TestFixture]
    public class ProfileFromWsTest : LoadJsonFileTest<WsMessageBase<Profile>> {
        public override string JsonSourceFileName => "Profiles\\profile-from-ws.json";

        [Test]
        public void LoadJsonFile_WsMessageOfProfile_Account() {
            //act
            var msg = ReadFileSource().Message;

            // assert
            msg.Balance.Should().Be(1031.2900390625m);
            msg.BalanceId.Should().Be(43997693);
            msg.BalanceType.Should().Be(4);
            msg.Balances.Should().NotBeEmpty();
        }


        [Test]
        public void LoadJsonFile_WsMessageOfProfile_Balance() {
            //act
            var msg = ReadFileSource().Message;

            // assert
            msg.Balance.Should().Be(1031.2900390625m);
            msg.BalanceId.Should().Be(43997693);
            msg.BalanceType.Should().Be(4);
        }


        [Test]
        public void LoadJsonFile_wsMessageOfProfile_Dates() {
            //act
            var msg = ReadFileSource().Message;

            // assert
            msg.Birthdate.Date.Should().Be(19.November(1988));
            msg.Created.Should().Be(5.January(2016));
        }

        [Test]
        public void LoadJsonFile_WsMessageOfProfile_MoneyShouldConverted() {
            //act
            var result = ReadFileSource();

            // assert
            result.Should().NotBeNull();
            result.Message.Should().NotBe(null);
        }

        [Test]
        public void LoadJsonFile_wsMessageOfProfile_User() {
            //act
            var msg = ReadFileSource().Message;

            // assert
            msg.UserId.Should().Be(7797866);
            msg.FirstName.Should().Be("Mongkon");
            msg.LastName.Should().Be("Eiadon");
            msg.Phone.Should().Be("66 *****3157");
            msg.Email.Should().Be("m@email.com");
            msg.Gender.Should().Be("male");
            msg.Address.Should().Be("Ramkamhang");
            msg.PostalIndex.Should().Be("10240");
        }
    }
}