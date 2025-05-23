package stepDefs;

import org.junit.After;
import org.junit.Before;
import utils.Driver;

public class Hooks {

    @Before
    public void setup(){
        /*
        Page page = Driver.get();
        page.setViewportSize(1920,1080);
         */
    }

    @After
    public void tearDown(){
        Driver.closeDriver();
    }
}
