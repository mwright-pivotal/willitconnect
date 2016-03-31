package willitconnect.controller;

import org.json.JSONObject;
import org.junit.Before;
import org.junit.Test;
import org.mockito.MockitoAnnotations;
import org.powermock.core.classloader.annotations.PrepareForTest;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import willitconnect.service.util.Connection;

import static org.hamcrest.Matchers.is;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;

public class WillItConnectV2ControllerTest {
    private MockMvc mockMvc;

    static JSONObject REQUEST = new JSONObject().put("url", "https://pivotal.io");

    @PrepareForTest(Connection.class)

    @Before
    public void setUp() {
        MockitoAnnotations.initMocks(this);
        mockMvc = MockMvcBuilders.standaloneSetup(
                new WillItConnectV2Controller()).build();
    }

    @Test
    public void itShouldConnectToAUrl() throws Exception {
        mockMvc.perform(get("/v2/willitconnect")
                    .contentType(MediaType.APPLICATION_JSON)
                    .content(REQUEST.toString()))
                .andExpect(jsonPath("$.canConnect", is(true)))
                .andExpect(jsonPath("$.entry", is(REQUEST.get("url"))));
    }

}