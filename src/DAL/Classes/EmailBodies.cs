using System;

namespace DAL.Classes
{
    public static class EmailBodies
    {
        public static string emailSetPasswordBody(string displayName, string linkParam)
        {
            Uri setPasswordPath = new Uri(DAL.DB.HostPath + "account/reset-password" + linkParam);

            string emailBody = "\n" +
                "<!DOCTYPE html PUBLIC>\n" +
                "<html  style=\"-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; height: 100%; width: 100%; margin: 0 auto; padding: 0;\">\n" +
                "\n" +
                "\n" +
                "<body width=\"100%\" style='-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; height: 100%; width: 100%; font-family: \"Helvetica Neue\", sans-serif; margin: 0 auto; padding: 0;' bgcolor=\"#f0f0f0\">\n" +
                "\n" +

                "<div class=\"row\">" +
                " <div class=\"col-12\">" +
                "        <table  class=\"body-wrap\" " +
                "        style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; " +
                "        box-sizing: border-box; font-size: 14px; width: 100%; background-color: #f9f8f7; margin: 0;\">" +
                "        <tr  style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif;" +
                "         box-sizing: border-box; font-size: 14px; margin: 0;\">" +
                "         <td  valign=\"top\" " +
                "            style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                "            font-size: 14px; vertical-align: top; margin: 0;\">" +
                "         </td>" +
                "         <td  width=\"600\" valign=\"top\" class=\"container\" " +
                "            style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px;" +
                "            vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;\">" +
                "            <div  class=\"content\" style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                "            font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;\">" +
                "                <table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" itemprop=\"action\" " +
                "                itemscope=\"\" itemtype=\"http://schema.org/ConfirmAction\" " +
                "                class=\"main\" style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; " +
                "                box-sizing: border-box; font-size: 14px; border-radius: 3px; margin: 0; border: none;\">" +
                "                    <tr style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                "                    font-size: 14px; margin: 0;\">" +
                "                        <td  valign=\"top\" class=\"content-wrap\" " +
                "                            style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                "                            color: #495057; font-size: 14px; vertical-align: top; margin: 0; padding: 30px; " +
                "                            box-shadow: 0 0.75rem 1.5rem rgba(18,38,63,.03); ;border-radius: 7px; background-color: #fff;\">" +
                "                                <meta  itemprop=\"name\" content=\"Confirm Email\" " +
                "                                    style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                "                                    font-size: 14px; margin: 0;\">" +
                "                                    <table  width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-family: " +
                "                                        'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                "                                        font-size: 14px; margin: 0;\">" +
                "                                        <tr  style=\"font-family: 'Helvetica Neue',Helvetica," +
                "                                            Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;\">" +
                "                                            <td  valign=\"top\" class=\"content-block\" " +
                "                                                style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; " +
                "                                                box-sizing: border-box; font-size: 14px; " +
                "                                                vertical-align: top; margin: 0; padding: 0 0 20px;\"> " +
                "                                                Dear " + displayName + " " +
                "                                            </td>" +
                "                                        </tr>" +
                "                                        <tr  style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; " +
                "                                            font-size: 14px; margin: 0;\">" +
                "                                            <td  valign=\"top\" class=\"content-block\" " +
                "                                                style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; " +
                "                                                box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;\"> " +
                "                                                Click the link below to set your password." +
                "                                            </td>" +
                "                                        </tr>" +
                "                                        <tr  style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;\">" +
                "                                            <td  itemprop=\"handler\" itemscope=\"\" itemtype=\"http://schema.org/HttpActionHandler\" valign=\"top\" class=\"content-block\" style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;\">" +
                "<a  href=" + setPasswordPath + " itemprop=\"url\" style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; text-transform: capitalize; background-color: #34c38f; margin: 0; border-color: #34c38f; border-style: solid; border-width: 8px 16px;\">" +
                "                                                    Set Password" +
                "                                                </a>" +
                "                                            </td>" +
                "                                        </tr>" +
                "                                        <tr  style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;\">" +
                "                                            <td  valign=\"top\" class=\"content-block\" style=\"font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;\">" +
                "                                                <strong>ACS</strong>" +
                "                                                <p>Support Team</p>" +
                "                                            </td>" +
                "                                        </tr>" +
                "                                    </table>" +
                "                                </td>" +
                "                            </tr>" +
                "                        </table>" +
                "                    </div>" +
                "                </td>" +
                "            </tr>" +
                "        </table>" +
                "    </div>" +
                "</div>" +

                "\n" +
                "<!-- <script data-cfasync=\"false\" src=\"/cdn-cgi/scripts/5c5dd728/cloudflare-static/email-decode.min.js\"></script> -->\n" +
                "</body>\n" +
                "</html>\n" +
                "\n";


            return emailBody;





        }
    }

}
